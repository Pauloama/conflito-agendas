using System.Collections.Concurrent;
using ConflitosAgenda.Api.Domain;

namespace ConflitosAgenda.Api.Infrastructure;

public interface ISolicitacaoStore
{
    Task<IReadOnlyCollection<Solicitacao>> ListarAsync(CancellationToken cancellationToken);
    Task<Solicitacao?> ObterAsync(Guid id, CancellationToken cancellationToken);
    Task AdicionarAsync(Solicitacao solicitacao, CancellationToken cancellationToken);
    Task AtualizarAsync(Solicitacao solicitacao, CancellationToken cancellationToken);
}

public interface IPlanoFocalStore
{
    Task<IReadOnlyCollection<PessoaCritica>> ListarAsync(CancellationToken cancellationToken);
    Task SubstituirAsync(IReadOnlyCollection<PessoaCritica> pessoas, CancellationToken cancellationToken);
}

public sealed class InMemorySolicitacaoStore : ISolicitacaoStore
{
    private readonly ConcurrentDictionary<Guid, Solicitacao> solicitacoes = new();

    public Task<IReadOnlyCollection<Solicitacao>> ListarAsync(CancellationToken cancellationToken) =>
        Task.FromResult<IReadOnlyCollection<Solicitacao>>(solicitacoes.Values.OrderBy(s => s.Inicio).ToArray());

    public Task<Solicitacao?> ObterAsync(Guid id, CancellationToken cancellationToken) =>
        Task.FromResult(solicitacoes.GetValueOrDefault(id));

    public Task AdicionarAsync(Solicitacao solicitacao, CancellationToken cancellationToken)
    {
        solicitacoes[solicitacao.Id] = solicitacao;
        return Task.CompletedTask;
    }

    public Task AtualizarAsync(Solicitacao solicitacao, CancellationToken cancellationToken)
    {
        solicitacoes[solicitacao.Id] = solicitacao;
        return Task.CompletedTask;
    }
}

public sealed class InMemoryPlanoFocalStore : IPlanoFocalStore
{
    private IReadOnlyCollection<PessoaCritica> pessoas = [];

    public Task<IReadOnlyCollection<PessoaCritica>> ListarAsync(CancellationToken cancellationToken) =>
        Task.FromResult(pessoas);

    public Task SubstituirAsync(IReadOnlyCollection<PessoaCritica> novasPessoas, CancellationToken cancellationToken)
    {
        pessoas = novasPessoas;
        return Task.CompletedTask;
    }
}
