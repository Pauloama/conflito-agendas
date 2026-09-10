using ConflitosAgenda.Api.Domain;

namespace ConflitosAgenda.Api.Application;

public sealed record CriarSolicitacaoRequest(
    Guid ColaboradorId,
    string ColaboradorNome,
    DateOnly Inicio,
    DateOnly Fim);

public sealed record DecidirSolicitacaoRequest(
    StatusSolicitacao Status,
    string CoordenadorId,
    string CoordenadorNome,
    string? Observacao);

public sealed record ImportarPlanoFocalRequest(
    IReadOnlyCollection<PessoaCriticaInput> Pessoas);

public sealed record PessoaCriticaInput(
    Guid PessoaId,
    string PessoaNome,
    Projeto Projeto);
