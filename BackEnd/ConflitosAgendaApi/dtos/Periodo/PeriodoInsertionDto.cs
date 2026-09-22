using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;
namespace ConflitosAgendaApi.Dtos.Periodos;
public class PeriodoInsertionDto    
{
    [Required]
    public DateTime DataInicio { get; private set; }
    [Required]
    public DateTime DataFim { get; private set; }
}