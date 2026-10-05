using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ConflitosAgendaApi.Dtos.Solicitacoes;
public class SolicitacaoInsertionDto
{
    [Required]
    public Colaborador Colaborador { get; private set; }
    [Required]
    [MinLength(1, ErrorMessage = "A solicitação deve conter pelo menos um período.")]
    [MaxLength(3, ErrorMessage = "A solicitação não pode conter mais de 3 períodos.")]
    public List<Periodo> Periodos { get; private set; } = new();
    public StatusSolicitacao Status { get; private set; } = StatusSolicitacao.Em_Analise;
    public DateTimeOffset CriadaEm { get; private set; } = DateTimeOffset.UtcNow;
    [Required]
    public Colaborador Coordenador { get; private set; }
    public string? Observacao { get; private set; }
    public int TotalDias { get; private set; }
    [Required]
    public bool Abono { get; private set; } = false;
    }