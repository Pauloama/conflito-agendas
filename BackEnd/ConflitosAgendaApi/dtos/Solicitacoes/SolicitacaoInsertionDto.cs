using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ConflitosAgendaApi.Dtos.Solicitacoes;
public class SolicitacaoInsertionDto
{
    public SolicitacaoInsertionDto()
    {
    }

    [Required]
    public Colaborador Colaborador { get; set; } = null!;
    [Required]
    [MinLength(1, ErrorMessage = "A solicitação deve conter pelo menos um período.")]
    [MaxLength(3, ErrorMessage = "A solicitação não pode conter mais de 3 períodos.")]
    public List<Periodo> Periodos { get; set; } = new();
    [Required]
    public StatusSolicitacao Status { get; set; } = StatusSolicitacao.Em_Analise;
    [Required]
    public DateTimeOffset CriadaEm { get; set; } = DateTimeOffset.UtcNow;
    [Required]
    public Colaborador Coordenador { get; set; } = null!;
    public string? Observacao { get; set; }
    [Required]
    public int TotalDias { get; set; }
    [Required]
    public bool Abono { get; set; } = false;
    [Required]
    public bool isPessoaCritica { get; set; } = false;

    public SolicitacaoInsertionDto(
        Solicitacao solicitacao)
    {
        Colaborador = solicitacao.Colaborador;
        Periodos = solicitacao.Periodos;
        Coordenador = solicitacao.Coordenador;
        Observacao = solicitacao.Observacao;
        TotalDias = solicitacao.TotalDias;
        Abono = solicitacao.Abono;
        this.isPessoaCritica = isPessoaCritica;
    }
    }