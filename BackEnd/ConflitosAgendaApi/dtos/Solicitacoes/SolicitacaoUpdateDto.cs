using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ConflitosAgendaApi.Dtos.Solicitacoes;

public class SolicitacaoUpdateDto
{
    public SolicitacaoUpdateDto()
    {
    }

    [Required]
    public Colaborador Colaborador { get; set; } = null!;
    [Required]
    [MinLength(1, ErrorMessage = "A solicitação deve conter pelo menos um período.")]
    [MaxLength(3, ErrorMessage = "A solicitação não pode conter mais de 3 períodos.")]
    public List<Periodo> Periodos { get; set; } = new();
    public StatusSolicitacao Status { get; set; }
    public DateTimeOffset CriadaEm { get; set; }
    [Required]
    public Colaborador Coordenador { get; set; } = null!;
    public string? Observacao { get; set; }
    public int TotalDias { get; set; }
    }