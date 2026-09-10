namespace ConflitosAgenda.Api.Domain;

public sealed record PessoaCritica(
    Guid PessoaId,
    string PessoaNome,
    string ProjetoId,
    string ProjetoNome);
