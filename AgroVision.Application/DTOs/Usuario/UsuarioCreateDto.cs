namespace AgroVision.Application.DTOs.Usuario;

public class UsuarioCreateDto
{
    public long Cpf { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string NomeFazenda { get; set; } = string.Empty;
}