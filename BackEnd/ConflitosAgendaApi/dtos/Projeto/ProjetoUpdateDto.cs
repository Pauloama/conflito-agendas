using System.ComponentModel.DataAnnotations;
using ConflitosAgendaApi.Models;

namespace ConflitosAgendaApi.Dtos.Projetos;

public class ProjetoUpdateDto
{
    [Required]
    public string Nome { get; set; } = string.Empty;
    [Required]
    public List<Colaborador> PessoasCriticas { get; set; } = new();
    [Required]
    public List<Colaborador> Colaboradores { get; set; } = new();
}