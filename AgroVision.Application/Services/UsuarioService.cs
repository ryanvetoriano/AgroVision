using AgroVision.Application.DTOs.Usuario;
using AgroVision.Application.Interfaces.Repositories;
using AgroVision.Application.Interfaces.Services;
using AgroVision.Domain.Entities;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> GetAllAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();

        return usuarios.Select(MapToResponse);
    }

    public async Task<UsuarioResponseDto?> GetByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);

        return usuario is null ? null : MapToResponse(usuario);
    }

    public async Task<UsuarioResponseDto> CreateAsync(UsuarioCreateDto dto)
    {
        var usuarioExistente = await _usuarioRepository.GetByCpfAsync(dto.Cpf);

        if (usuarioExistente is not null)
            throw new DomainException("Já existe um usuário cadastrado com este CPF.");

        var usuario = new Usuario(
            dto.Cpf,
            dto.Nome,
            dto.Senha,
            dto.NomeFazenda);

        await _usuarioRepository.AddAsync(usuario);
        await _usuarioRepository.SaveChangesAsync();

        return MapToResponse(usuario);
    }

    public async Task<UsuarioResponseDto?> UpdateAsync(int id, UsuarioUpdateDto dto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);

        if (usuario is null)
            return null;

        usuario.Atualizar(
            dto.Nome,
            dto.Senha,
            dto.NomeFazenda);

        _usuarioRepository.Update(usuario);
        await _usuarioRepository.SaveChangesAsync();

        return MapToResponse(usuario);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);

        if (usuario is null)
            return false;

        _usuarioRepository.Delete(usuario);
        await _usuarioRepository.SaveChangesAsync();

        return true;
    }

    private static UsuarioResponseDto MapToResponse(Usuario usuario)
    {
        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Cpf = usuario.Cpf,
            Nome = usuario.Nome,
            NomeFazenda = usuario.NomeFazenda,
            TotalPlantacoes = usuario.Plantacoes?.Count ?? 0
        };
    }
}