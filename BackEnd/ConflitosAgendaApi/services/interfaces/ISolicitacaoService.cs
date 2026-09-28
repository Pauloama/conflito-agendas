namespace ConflitosAgendaApi.Services.Interfaces;
using ConflitosAgendaApi.Models;

public interface ISolicitacaoService
{
    Task<List<Solicitacao>> GetAllAsync();
    Task<Solicitacao> GetByIdAsync(int id);
    Task<List<Solicitacao>> GetByColaboradorIdAsync(int colaboradorId);
    Task<List<Solicitacao>> GetByPeriodoIdAsync(int periodoId);
    Task<List<Solicitacao>> GetByStatusAsync(StatusSolicitacao status);
    Task AddAsync(Solicitacao solicitacao);
    Task UpdateAsync(Solicitacao solicitacao);
    Task DeleteAsync(int id);
    Task<bool> ValidateSolicitacaoAsync(int solicitacaoId);
}