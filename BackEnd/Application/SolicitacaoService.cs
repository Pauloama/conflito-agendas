using ConflitosAgenda.Api.Domain;
using ConflitosAgenda.Api.Infrastructure;

namespace ConflitosAgenda.Api.Application;

public sealed class SolicitacaoService(
    ISolicitacaoStore solicitacaoStore,
    IPlanoFocalStore planoFocalStore)
{
    public Task<IReadOnlyCollection<Solicitacao>> ListarAsync(CancellationToken cancellationToken) =>
        solicitacaoStore.ListarAsync(cancellationToken);

    public async Task<Result<Solicitacao>> CriarAsync(
        CriarSolicitacaoRequest request,
        CancellationToken cancellationToken)
    {
        var periodoValido = request.Inicio <= request.Fim;
        if (!periodoValido)
        {
            return Result<Solicitacao>.Falha("A data de início deve ser anterior ou igual à data de fim.");
        }

        var solicitacao = new Solicitacao(
            Guid.NewGuid(),
            request.ColaboradorId,
            request.ColaboradorNome,
            request.Inicio,
            request.Fim);

        var conflito = await EncontrarConflitoAsync(solicitacao, cancellationToken);
        if (conflito is not null)
        {
            return Result<Solicitacao>.Falha(
                $"Existe conflito com a solicitação {conflito.Id} de outra pessoa crítica do projeto {conflito.ProjetoNome}.");
        }

        await solicitacaoStore.AdicionarAsync(solicitacao, cancellationToken);
        return Result<Solicitacao>.Ok(solicitacao);
    }

    public async Task<Result<Solicitacao>> DecidirAsync(
        Guid id,
        DecidirSolicitacaoRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Status is not (StatusSolicitacao.Aprovada or StatusSolicitacao.Recusada))
        {
            return Result<Solicitacao>.Falha("A decisão deve ser Aprovada ou Recusada.");
        }

        var solicitacao = await solicitacaoStore.ObterAsync(id, cancellationToken);
        if (solicitacao is null)
        {
            return Result<Solicitacao>.NotFound();
        }

        if (request.Status == StatusSolicitacao.Aprovada)
        {
            var conflito = await EncontrarConflitoAsync(solicitacao, cancellationToken);
            if (conflito is not null)
            {
                return Result<Solicitacao>.Falha(
                    $"A aprovação criaria conflito com a solicitação {conflito.Id} de {conflito.ColaboradorNome}.");
            }
        }

        solicitacao.Decidir(request.Status, request.CoordenadorId, request.CoordenadorNome, request.Observacao);
        await solicitacaoStore.AtualizarAsync(solicitacao, cancellationToken);
        return Result<Solicitacao>.Ok(solicitacao);
    }

    private async Task<Conflito?> EncontrarConflitoAsync(
        Solicitacao solicitacao,
        CancellationToken cancellationToken)
    {
        var pessoas = await planoFocalStore.ListarAsync(cancellationToken);
        var projetosDaPessoa = pessoas
            .Where(p => p.PessoaId == solicitacao.ColaboradorId)
            .Select(p => p.ProjetoId)
            .ToHashSet();

        if (projetosDaPessoa.Count == 0)
        {
            return null;
        }

        var solicitacoes = await solicitacaoStore.ListarAsync(cancellationToken);
        var outraSolicitacao = solicitacoes.FirstOrDefault(s =>
            s.Id != solicitacao.Id &&
            s.Status is StatusSolicitacao.Pendente or StatusSolicitacao.Aprovada &&
            s.Inicio <= solicitacao.Fim &&
            solicitacao.Inicio <= s.Fim &&
            pessoas.Any(p => p.PessoaId == s.ColaboradorId && projetosDaPessoa.Contains(p.ProjetoId)));

        if (outraSolicitacao is null)
        {
            return null;
        }

        var projeto = pessoas.First(p =>
            p.PessoaId == solicitacao.ColaboradorId &&
            pessoas.Any(outro => outro.PessoaId == outraSolicitacao.ColaboradorId && outro.ProjetoId == p.ProjetoId));

        return new Conflito(outraSolicitacao.Id, projeto.ProjetoNome, outraSolicitacao.ColaboradorNome);
    }
}

public sealed record Conflito(Guid Id, string ProjetoNome, string ColaboradorNome);
