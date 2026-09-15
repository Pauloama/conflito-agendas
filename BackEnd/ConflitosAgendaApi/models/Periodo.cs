public class Periodo
{
    public Periodo()
    {
    }
    public Periodo(DateTime dataInicio, DateTime dataFim)
    {
            DataInicio = dataInicio;
            DataFim = dataFim;   
    }
    public Guid Id { get; private set;} = Guid.NewGuid();
    public DateTime DataInicio { get; private set; }
    public DateTime DataFim { get; private set; }
    
}