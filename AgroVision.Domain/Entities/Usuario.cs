using AgroVision.Domain.Common;
using AgroVision.Domain.Exceptions;

namespace AgroVision.Domain.Entities;

public class Usuario : BaseEntity
{
    public long Cpf { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Senha { get; private set; } = string.Empty;
    public string NomeFazenda { get; private set; } = string.Empty;

    public ICollection<Plantacao> Plantacoes { get; private set; } = new List<Plantacao>();

    protected Usuario()
    {
    }

    public Usuario(long cpf, string nome, string senha, string nomeFazenda)
    {
        Validar(cpf, nome, senha, nomeFazenda);

        Cpf = cpf;
        Nome = nome;
        Senha = senha;
        NomeFazenda = nomeFazenda;
    }

    public void Atualizar(string nome, string senha, string nomeFazenda)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("O nome do usuário é obrigatório.");

        if (string.IsNullOrWhiteSpace(senha))
            throw new DomainException("A senha é obrigatória.");

        if (string.IsNullOrWhiteSpace(nomeFazenda))
            throw new DomainException("O nome da fazenda é obrigatório.");

        Nome = nome;
        Senha = senha;
        NomeFazenda = nomeFazenda;
    }

    private static void Validar(long cpf, string nome, string senha, string nomeFazenda)
    {
        if (cpf <= 0)
            throw new DomainException("O CPF é obrigatório.");

        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("O nome do usuário é obrigatório.");

        if (string.IsNullOrWhiteSpace(senha))
            throw new DomainException("A senha é obrigatória.");

        if (senha.Length > 18)
            throw new DomainException("A senha deve ter no máximo 18 caracteres.");

        if (string.IsNullOrWhiteSpace(nomeFazenda))
            throw new DomainException("O nome da fazenda é obrigatório.");
    }
}