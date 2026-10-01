namespace ConflitosAgendaApi.Services.Interfaces;

using ConflitosAgendaApi.Dtos.Colaboradores;
using ConflitosAgendaApi.Models;

public interface IColaboradorService
{
    Task<ColaboradorResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<ColaboradorResponseDto>> ListAllAsync();
    Task<ColaboradorInsertionDto?> AddAsync(Colaborador colaborador);
    Task<ColaboradorUpdateDto?> UpdateAsync(Colaborador colaborador);
    Task<bool?> DeleteAsync(int id);
    Task<ColaboradorUpdateDto?> TornarCoordenadorAsync(int id);
}