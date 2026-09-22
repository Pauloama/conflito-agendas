namespace ConflitosAgendaApi.Services.Interfaces;
using ConflitosAgendaApi.Models;

public interface ISolicitacaoService
{
    Task<List<Solicitacao>> GetAllAsync();
    Task<Solicitacao> GetByIdAsync(Guid id);
    Task<List<Solicitacao>> GetByColaboradorIdAsync(Guid colaboradorId);
    Task<List<Solicitacao>> GetByPeriodoIdAsync(Guid periodoId);
    Task<List<Solicitacao>> GetByStatusAsync(StatusSolicitacao status);
    Task AddAsync(Solicitacao solicitacao);
    Task UpdateAsync(Solicitacao solicitacao);
    Task DeleteAsync(int id);
}