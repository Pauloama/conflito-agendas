namespace ConflitosAgendaApi.Models;
public sealed class Solicitacao
{
    public Solicitacao()
    {
    }
    public Solicitacao(
        Colaborador colaborador,
        List<Periodo> periodos,
        Colaborador coordenador
    )
    {
        this.Colaborador = colaborador;
        this.Periodos = periodos;
        this.Coordenador = coordenador;
    }
    public Guid Id { get; private set;} = Guid.NewGuid();
    public Colaborador Colaborador { get; private set; }
    public List<Periodo> Periodos { get; private set; } = new();
    public StatusSolicitacao Status { get; private set; } = StatusSolicitacao.Em_Analise;
    public DateTimeOffset CriadaEm { get; private set; } = DateTimeOffset.UtcNow;
    public Colaborador Coordenador { get; private set; }
    public string? Observacao { get; private set; }
    public bool Abono { get; private set; } = false;
    public bool isPessoaCritica { get; private set; } = false;

    public void Aprovar()
    {
        this.Status = StatusSolicitacao.Aprovada;
    }

    public void Reprovar(string observacao)
    {
        this.Status = StatusSolicitacao.Recusada;
        this.Observacao = observacao;
    }
}
