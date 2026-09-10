namespace ConflitosAgenda.Api.Domain;

public sealed class Coordenador(
    Guid Id,
    string Nome
)
{
    public Guid Id { get; } = Id;
    public string Nome { get; } = Nome;
}