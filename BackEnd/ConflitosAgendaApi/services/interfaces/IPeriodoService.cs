namespace ConflitosAgendaApi.Services.Interfaces;
using ConflitosAgendaApi.Models;

public interface IPeriodoService
{
    Task<Periodo?> GetByIdAsync(int id);
    Task<IEnumerable<Periodo>> ListAllAsync();
    Task<Periodo?> AddAsync(Periodo periodo);
    Task<Periodo?> UpdateAsync(Periodo periodo);
    Task<Periodo?> DeleteAsync(int id);
    Task<IEnumerable<Periodo>> ListByColaboradorIdAsync(int colaboradorId);
    Task<bool> ValidarPeriodoAsync(Periodo periodo);
}