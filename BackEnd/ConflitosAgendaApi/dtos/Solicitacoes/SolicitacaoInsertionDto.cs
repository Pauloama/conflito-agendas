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
    [Required]
    public StatusSolicitacao Status { get; private set; } = StatusSolicitacao.Em_Analise;
    [Required]
    public DateTimeOffset CriadaEm { get; private set; } = DateTimeOffset.UtcNow;
    [Required]
    public Colaborador Coordenador { get; private set; }
    public string? Observacao { get; private set; }
    [Required]
    public int TotalDias { get; private set; }
    [Required]
    public bool Abono { get; private set; } = false;
    [Required]
    public bool isPessoaCritica { get; private set; } = false;

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