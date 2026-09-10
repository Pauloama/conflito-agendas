namespace ConflitosAgenda.Api.Domain;
public sealed class Projeto(
    Guid id,
    string nome
)
{
    public Guid Id { get; } = id;
    public string Nome { get; } = nome;
}