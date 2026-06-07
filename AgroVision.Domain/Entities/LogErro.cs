using AgroVision.Domain.Common;

namespace AgroVision.Domain.Entities;

public class LogErro : BaseEntity
{
    public string? NomeProcedure { get; private set; }
    public string? NomeUsuario { get; private set; }
    public DateTime DataErro { get; private set; }
    public int CodigoErro { get; private set; }
    public string? MensagemErro { get; private set; }

    protected LogErro()
    {
    }

    public LogErro(string? nomeProcedure, string? nomeUsuario, int codigoErro, string? mensagemErro)
    {
        NomeProcedure = nomeProcedure;
        NomeUsuario = nomeUsuario;
        DataErro = DateTime.Now;
        CodigoErro = codigoErro;
        MensagemErro = mensagemErro;
    }
}