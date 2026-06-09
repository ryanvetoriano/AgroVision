using AgroVision.Application.DTOs.Usuario;

namespace AgroVision.Application.Interfaces.Services;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponseDto>> GetAllAsync();
    Task<UsuarioResponseDto?> GetByIdAsync(int id);
    Task<UsuarioResponseDto> CreateAsync(UsuarioCreateDto dto);
    Task<UsuarioResponseDto?> UpdateAsync(int id, UsuarioUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}