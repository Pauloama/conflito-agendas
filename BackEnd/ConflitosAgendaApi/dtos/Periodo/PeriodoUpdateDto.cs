using ConflitosAgendaApi.Models;
using System.ComponentModel.DataAnnotations;
namespace ConflitosAgendaApi.Dtos.Periodos;
public class PeriodoUpdateDto
{
    [Required]
    public DateTime DataInicio { get; private set; }
    [Required]
    public DateTime DataFim { get; private set; }
    public int IdColaborador { get; private set; }
    public PeriodoUpdateDto(Periodo periodo)
    {
        DataInicio = periodo.DataInicio;
        DataFim = periodo.DataFim;
    }
}