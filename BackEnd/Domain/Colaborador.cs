namespace ConflitosAgenda.Api.Domain;

public class Colaborador(
    Guid Id,
    string Nome,
    string Email,
    Projeto Projeto)
{
    public Guid Id { get; } = Id;
    public string Nome { get; } = Nome;
    public string Email { get; } = Email;
    public Guid ProjetoId { get; } = Projeto.Id;
    public string ProjetoNome { get; } = Projeto.Nome;
}