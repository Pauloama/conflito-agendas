using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;
namespace ConflitosAgendaApi.Dtos.Colaboradores;
public class ColaboradorResponseDto
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public List<Projeto> Projetos { get; private set; } = new();
    public bool IsCoordenador { get; private set; } = false;
}