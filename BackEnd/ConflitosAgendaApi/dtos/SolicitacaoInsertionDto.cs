using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ConflitosAgendaApi.Dtos;
public class SolicitacaoInsertionDto
{
    [Required]
    public Colaborador Colaborador { get; private set; }

    [Required]
    public List<Periodo> Periodos { get; private set; } = new();
    public StatusSolicitacao Status { get; private set; } = StatusSolicitacao.Em_Analise;
    public DateTimeOffset CriadaEm { get; private set; } = DateTimeOffset.UtcNow;
    [Required]
    public Colaborador Coordenador { get; private set; }
    public string? Observacao { get; private set; }

    [Required]
    public bool isPessoaCritica { get; private set; } = false;
    }