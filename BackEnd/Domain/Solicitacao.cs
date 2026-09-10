namespace ConflitosAgenda.Api.Domain;

public sealed class Solicitacao(
    Guid id,
    Guid colaboradorId,
    string colaboradorNome,
    DateOnly inicio,
    DateOnly fim)
{
    public Guid Id { get; } = id;
    public Guid ColaboradorId { get; } = colaboradorId;
    public string ColaboradorNome { get; } = colaboradorNome;
    public DateOnly Inicio { get; } = inicio;
    public DateOnly Fim { get; } = fim;
    public StatusSolicitacao Status { get; private set; } = StatusSolicitacao.Em_Analise;
    public DateTimeOffset CriadaEm { get; } = DateTimeOffset.UtcNow;
    public Guid CoordenadorId { get; private set; }
    public string? CoordenadorNome { get; private set; }
    public string? Observacao { get; private set; }

    public void Decidir(
        StatusSolicitacao status,
        Coordenador coordenador,
        string? observacao)
    {
        Status = status;
        CoordenadorId = coordenador.Id;
        CoordenadorNome = coordenador.Nome;
        Observacao = observacao;
    }
}
