namespace ConflitosAgendaApi.Services.Interfaces;
using ConflitosAgendaApi.Dtos.Projetos;
using ConflitosAgendaApi.Dtos.Colaboradores;

public interface IProjetoService
{
    Task<ProjetoResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<ProjetoResponseDto>> ListAllAsync();
    Task<ProjetoInsertionDto?> AddAsync(ProjetoInsertionDto projeto);
    Task<ProjetoUpdateDto?> UpdateAsync(ProjetoUpdateDto projeto);
    Task<bool?> DeleteAsync(int id);
    Task<IEnumerable<ColaboradorResponseDto>> ListColaboradoresAsync(int projetoId);

}