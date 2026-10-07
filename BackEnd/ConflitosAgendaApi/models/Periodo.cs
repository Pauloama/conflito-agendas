namespace ConflitosAgendaApi.Models;
public sealed class Periodo
{
    public Periodo()
    {
    }
    public Periodo(DateTime dataInicio, DateTime dataFim)
    {
            DataInicio = dataInicio;
            DataFim = dataFim;   
    }
    public int Id { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime DataFim { get; private set; }
    public int IdColaborador { get; private set; }
    
}