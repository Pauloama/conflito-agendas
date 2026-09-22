using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ConflitosAgendaApi.Dtos.Solicitacoes;

public class SolicitacaoResponseDto
{
    public Guid Id { get; private set;} = Guid.NewGuid();

    public Colaborador Colaborador { get; private set; }
    public List<Periodo> Periodos { get; private set; } = new();
    public StatusSolicitacao Status { get; private set; } = StatusSolicitacao.Em_Analise;
    public DateTimeOffset CriadaEm { get; private set; } = DateTimeOffset.UtcNow;
    public Colaborador Coordenador { get; private set; }
    public string? Observacao { get; private set; }
    public int TotalDias { get; private set; }
    public bool Abono { get; private set; } = false;
    public bool isPessoaCritica { get; private set; } = false;
}