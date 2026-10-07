namespace ConflitosAgendaApi.Models;
public sealed class Solicitacao
{
    public Solicitacao()
    {
    }
    public Solicitacao(
        Colaborador colaborador,
        List<Periodo> periodos,
        StatusSolicitacao status,
        DateTimeOffset criadaEm,
        Colaborador coordenador,
        string? observacao = null,
        bool abono = false,
        bool isPessoaCritica = false,
        int totalDias = 0
    )
    {
        this.Colaborador = colaborador;
        this.Periodos = periodos;
        this.Coordenador = coordenador;
        this.Status = status;
        this.CriadaEm = criadaEm;
        this.Observacao = observacao;
        this.Abono = abono;
        this.isPessoaCritica = isPessoaCritica;
        this.TotalDias = totalDias;
    }
    public int Id { get; private set; }
    public Colaborador Colaborador { get; private set; }
    public List<Periodo> Periodos { get; private set; } = new();
    public StatusSolicitacao Status { get; private set; } = StatusSolicitacao.Em_Analise;
    public DateTimeOffset CriadaEm { get; private set; } = DateTimeOffset.UtcNow;
    public Colaborador Coordenador { get; private set; }
    public int TotalDias { get; private set; }
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

    public void Atualizar(
        Colaborador colaborador,
        List<Periodo> periodos,
        Colaborador coordenador,
        StatusSolicitacao status,
        string? observacao)
    {
        Colaborador = colaborador;
        Periodos = periodos;
        Coordenador = coordenador;
        Status = status;
        Observacao = observacao;
    }
}
