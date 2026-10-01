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

    public async Task<PeriodoResponseDto?> GetByIdAsync(int id)
    {
        var periodo = await _periodoRepository.GetByIdAsync(id);
        if (periodo == null)
        {
            return null;
        }
        return new PeriodoResponseDto(periodo);
    }
    public async Task<IEnumerable<PeriodoResponseDto>> ListAllAsync()
    {
        var periodos = await _periodoRepository.ListAllAsync();
        return periodos.Select(p => new PeriodoResponseDto(p));
    }
    public async Task<PeriodoInsertionDto?> AddAsync(PeriodoInsertionDto periodo)
    {
        var novoPeriodo = new Periodo(periodo.DataInicio, periodo.DataFim);
        var periodoAdicionado = await _periodoRepository.AddAsync(novoPeriodo);
        if (periodoAdicionado == null)
        {
            return null;
        }
        return new PeriodoInsertionDto(periodoAdicionado);
    }
    public async Task<PeriodoUpdateDto?> UpdateAsync(PeriodoUpdateDto periodo)
    {
        var periodoAtualizado = await _periodoRepository.UpdateAsync(new Periodo(periodo.DataInicio, periodo.DataFim));
        if (periodoAtualizado == null)
        {
            return null;
        }
        return new PeriodoUpdateDto(periodoAtualizado);
    }
    public async Task<bool?> DeleteAsync(int id)
    {
        var periodo = await _periodoRepository.GetByIdAsync(id);
        if (periodo == null)
        {
            return null;
        }
        return true;
    }
    public async Task<IEnumerable<PeriodoResponseDto>> ListByColaboradorIdAsync(int colaboradorId)
    {
        var periodos = await _periodoRepository.ListByColaboradorIdAsync(colaboradorId);
        return periodos.Select(p => new PeriodoResponseDto(p));
    }
    public async Task<bool> ValidarPeriodoAsync(PeriodoResponseDto periodo)
    {
        return await _periodoRepository.ValidarPeriodoAsync(new Periodo(periodo.DataInicio, periodo.DataFim));
    }

}