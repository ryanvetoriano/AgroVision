namespace AgroVision.Application.DTOs.Usuario;

public class UsuarioResponseDto
{
    public int Id { get; set; }
    public long Cpf { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string NomeFazenda { get; set; } = string.Empty;
    public int TotalPlantacoes { get; set; }
}