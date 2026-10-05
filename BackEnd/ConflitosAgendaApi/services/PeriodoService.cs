using ConflitosAgendaApi.Dtos.Periodos;
using ConflitosAgendaApi.Models;
using ConflitosAgendaApi.Services.Interfaces;
using ConflitosAgendaApi.Repositories.Interfaces;

namespace ConflitosAgendaApi.Services;

public class PeriodoService : IPeriodoService
{
    private readonly IPeriodoRepository _periodoRepository;

    public PeriodoService(IPeriodoRepository periodoRepository)
    {
        _periodoRepository = periodoRepository;
    }

    public async Task<Periodo?> GetByIdAsync(int id)
    {
        var periodo = await _periodoRepository.GetByIdAsync(id);

        if (periodo == null)
        {
            return null;
        }
        
        return periodo;
    }
    public async Task<IEnumerable<Periodo>> ListAllAsync()
    {
        return await _periodoRepository.ListAllAsync();
    }
    public async Task<Periodo?> AddAsync(PeriodoInsertionDto periodo)
    {
        if (periodo == null)
        {
            throw new ArgumentNullException(nameof(periodo));
        }

        var resultadoValidacao = await ValidarPeriodoAsync(periodo.DataInicio, periodo.DataFim);

        if (!resultadoValidacao)
        {
            throw new ArgumentException("A data de início deve ser anterior à data de fim.");
        }

        var novoPeriodo = await _periodoRepository.AddAsync(new Periodo(periodo.DataInicio, periodo.DataFim));

        return novoPeriodo;
    }
    public async Task<Periodo?> UpdateAsync(PeriodoUpdateDto periodo)
    {
        if (periodo == null)
        {
            throw new ArgumentNullException(nameof(periodo));
        }

        var resultadoValidacao = await ValidarPeriodoAsync(periodo.DataInicio, periodo.DataFim);

        if (!resultadoValidacao)
        {
            throw new ArgumentException("A data de início deve ser anterior à data de fim.");
        }

        var periodoAtualizado = await _periodoRepository.UpdateAsync(new Periodo(periodo.DataInicio, periodo.DataFim));

        return periodoAtualizado;
    }
    public async Task<bool?> DeleteAsync(int id)
    {
        var periodo = await _periodoRepository.GetByIdAsync(id);

        if (periodo == null)
        {
            throw new ArgumentNullException("O período informado não existe.");
        }

        return true;
    }
    public async Task<IEnumerable<Periodo>> ListByColaboradorIdAsync(int colaboradorId)
    {
        return await _periodoRepository.ListByColaboradorIdAsync(colaboradorId);
    }
    public async Task<bool> ValidarPeriodoAsync(int periodoId)
    {
        var periodo = await _periodoRepository.GetByIdAsync(periodoId);

        if (periodo == null)
        {
            return false;
        }

        return true;
        
    }

    public async Task<bool> ValidarPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        if (dataInicio >= dataFim)
        {
            throw new ArgumentException("A data de início deve ser anterior à data de fim.");
        }
        return true;
    }


}