namespace ConflitosAgendaApi.Services.Interfaces;
using ConflitosAgendaApi.Dtos.Periodos;

public interface IPeriodoService
{
    Task<PeriodoResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<PeriodoResponseDto>> ListAllAsync();
    Task<PeriodoInsertionDto?> AddAsync(PeriodoInsertionDto periodo);
    Task<PeriodoUpdateDto?> UpdateAsync(PeriodoUpdateDto periodo);
    Task<bool?> DeleteAsync(int id);
    Task<IEnumerable<PeriodoResponseDto>> ListByColaboradorIdAsync(int colaboradorId);
    Task<bool> ValidarPeriodoAsync(int periodoId);
}