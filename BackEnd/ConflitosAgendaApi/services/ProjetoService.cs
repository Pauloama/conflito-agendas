namespace ConflitosAgendaApi.Services;
using ConflitosAgendaApi.Dtos.Projetos;
using ConflitosAgendaApi.Dtos.Colaboradores;
using ConflitosAgendaApi.Models;

using ConflitosAgendaApi.Repositories.Interfaces;
using ConflitosAgendaApi.Services.Interfaces;

public class ProjetoService : IProjetoService
{
    private readonly IProjetoRepository _projetoRepository;
    private readonly IColaboradorRepository _colaboradorRepository;

    public ProjetoService(IProjetoRepository projetoRepository, IColaboradorRepository colaboradorRepository)
    {
        _projetoRepository = projetoRepository;
        _colaboradorRepository = colaboradorRepository;
    }

    public async Task<ProjetoResponseDto?> GetByIdAsync(int id)
    {
        var projeto = await _projetoRepository.GetByIdAsync(id);
        if (projeto == null) return null;
        return new ProjetoResponseDto(projeto);
    }

    public async Task<IEnumerable<ProjetoResponseDto>> ListAllAsync()
    {
        var projetos = await _projetoRepository.ListAllAsync();
        return projetos.Select(p => new ProjetoResponseDto(p));
    }

    public async Task<ProjetoInsertionDto?> AddAsync(ProjetoInsertionDto projeto)
    {
        var newProjeto = new Projeto(projeto.Nome, projeto.Descricao);
        var addedProjeto = await _projetoRepository.AddAsync(newProjeto);
        if (addedProjeto == null) return null;
        return new ProjetoInsertionDto(addedProjeto);
    }

    public async Task<ProjetoUpdateDto?> UpdateAsync(ProjetoUpdateDto projeto)
    {
        var existingProjeto = await _projetoRepository.GetByIdAsync(projeto.Id);
        if (existingProjeto == null) return null;

        existingProjeto.Update(projeto);
        var updatedProjeto = await _projetoRepository.UpdateAsync(existingProjeto);
        if (updatedProjeto == null) return null;
        return new ProjetoUpdateDto(updatedProjeto);
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        var deleted = await _projetoRepository.DeleteAsync(id);
        return deleted != null;
    }

    public async Task<IEnumerable<ColaboradorResponseDto>> ListColaboradoresAsync(int projetoId)
    {
        var colaboradores = await _colaboradorRepository.ListByProjetoIdAsync(projetoId);
        return colaboradores.Select(c => new ColaboradorResponseDto(c));
    }
}