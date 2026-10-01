using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;
namespace ConflitosAgendaApi.Dtos.Periodos;
public class PeriodoInsertionDto    
{
    [Required]
    public DateTime DataInicio { get; private set; }
    [Required]
    public DateTime DataFim { get; private set; }
    public int IdColaborador { get; private set; }

    public PeriodoInsertionDto(Periodo periodo)
    {
        DataInicio = periodo.DataInicio;
        DataFim = periodo.DataFim;
    }
}