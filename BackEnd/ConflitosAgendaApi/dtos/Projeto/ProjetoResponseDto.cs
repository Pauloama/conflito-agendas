using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ConflitosAgendaApi.Dtos.Projetos;

public class ProjetoResponseDto
{
    [Required]
    public int Id { get; private set; }
    [Required]
    public string Nome { get; private set; } = string.Empty;
    [Required]
    public List<Colaborador> PessoasCriticas { get; private set; } = new();
    [Required]
    public List<Colaborador> Colaboradores { get; private set; } = new();
}