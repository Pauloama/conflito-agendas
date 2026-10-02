namespace ConflitosAgendaApi.Services.Interfaces;

using ConflitosAgendaApi.Dtos.Colaboradores;
public interface IColaboradorService
{
    Task<ColaboradorResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<ColaboradorResponseDto>> ListAllAsync();
    Task<ColaboradorResponseDto?> AddAsync(ColaboradorInsertionDto colaborador);
    Task<ColaboradorResponseDto?> UpdateAsync(int id, ColaboradorUpdateDto colaborador);
    Task<bool?> DeleteAsync(int id);
    Task<ColaboradorResponseDto?> TornarCoordenadorAsync(int id, ColaboradorUpdateDto colaborador);
}