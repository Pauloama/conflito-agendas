using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ConflitosAgendaApi.Dtos.Colaboradores;

public class ColaboradorInsertionDto
{
    [Required]
    public string Nome { get; private set; }
    [Required]
    [EmailAddress]
    public string Email { get; private set; }
    [Required]
    public List<Projeto> Projetos { get; private set; } = new();
    [Required]
    public bool IsCoordenador { get; private set; } = false;
}