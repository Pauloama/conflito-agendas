namespace ConflitosAgendaApi.Repositories;

using ConflitosAgendaApi.Models;
using ConflitosAgendaApi.Repositories.Interfaces;
using ConflitosAgendaApi.Data;
using Microsoft.EntityFrameworkCore;
public class SolicitacaoRepository : iSolicitacaoRepository
{
    private readonly AppDbContext _context;

    public SolicitacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Solicitacao> GetByIdAsync(Guid id)
    {
        var solicitacao = await _context.Solicitacoes
            .Include(s => s.Colaborador)
            .Include(s => s.Periodos)
            .Include(s => s.Coordenador)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (solicitacao == null)
        {
            throw new KeyNotFoundException($"Solicitação com ID {id} não encontrada.");
        }
        return solicitacao;
    }

    public async Task<List<Solicitacao>> GetAllAsync()
    {
        var list = await _context.Solicitacoes
            .Include(s => s.Colaborador)
            .Include(s => s.Periodos)
            .Include(s => s.Coordenador)
            .ToListAsync();
        
        if (list == null || list.Count == 0)
        {
            throw new KeyNotFoundException("Nenhuma solicitação encontrada.");
        }
            return list;
    }

    public async Task<List<Solicitacao>> GetByColaboradorIdAsync(Guid colaboradorId)
    {
        return await _context.Solicitacoes
            .Include(s => s.Colaborador)
            .Include(s => s.Periodos)
            .Include(s => s.Coordenador)
            .Where(s => s.Colaborador.Id == colaboradorId)
            .ToListAsync();
    }

    public async Task<List<Solicitacao>> GetByStatusAsync(StatusSolicitacao status)
    {
        return await _context.Solicitacoes
            .Include(s => s.Colaborador)
            .Include(s => s.Periodos)
            .Include(s => s.Coordenador)
            .Where(s => s.Status == status)
            .ToListAsync();
    }

    public async Task AddAsync(Solicitacao solicitacao)
    {
        await _context.Solicitacoes.AddAsync(solicitacao);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Solicitacao solicitacao)
    {
        _context.Solicitacoes.Update(solicitacao);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Solicitacao solicitacao)
    {
        _context.Solicitacoes.Remove(solicitacao);
        await _context.SaveChangesAsync();
    }
}