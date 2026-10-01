using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;
namespace ConflitosAgendaApi.Dtos.Projetos;

public class ProjetoInsertionDto
{
    [Required]
    public string Nome { get; set; } = string.Empty;
    [Required]
    public List<Colaborador> PessoasCriticas { get; set; } = new();
    [Required]
    public List<Colaborador> Colaboradores { get; set; } = new();
}