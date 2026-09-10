using ConflitosAgenda.Api.Domain;
using ConflitosAgenda.Api.Infrastructure;

namespace ConflitosAgenda.Api.Application;

public sealed class PlanoFocalService(IPlanoFocalStore store)
{
    public Task<IReadOnlyCollection<PessoaCritica>> ListarAsync(CancellationToken cancellationToken) =>
        store.ListarAsync(cancellationToken);

    public async Task<Result<IReadOnlyCollection<PessoaCritica>>> ImportarAsync(
        ImportarPlanoFocalRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Pessoas.Count == 0)
        {
            return Result<IReadOnlyCollection<PessoaCritica>>.Falha(
                "O plano focal deve conter ao menos uma pessoa.");
        }

        var duplicados = request.Pessoas
            .GroupBy(p => new { p.PessoaId, p.ProjetoId })
            .Where(g => g.Count() > 1)
            .Select(g => $"{g.Key.PessoaId}/{g.Key.ProjetoId}")
            .ToArray();

        if (duplicados.Length > 0)
        {
            return Result<IReadOnlyCollection<PessoaCritica>>.Falha(
                $"Há vínculos duplicados no plano focal: {string.Join(", ", duplicados)}.");
        }

        var pessoas = request.Pessoas
            .Select(p => new PessoaCritica(p.PessoaId, p.PessoaNome, p.Projeto))
            .ToArray();

        await store.SubstituirAsync(pessoas, cancellationToken);
        return Result<IReadOnlyCollection<PessoaCritica>>.Ok(pessoas);
    }
}
