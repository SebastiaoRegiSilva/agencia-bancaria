namespace Agencia.Plataforma.Domain.Clients
{
    /// <summary>Possíveis tipos de cliente.</summary>
    public enum ClientType
    {
        /// <summary>Tipo de cadastro que utiliza o CNPJ como chave.<summary>
        PessoaJuridica = 0,

        /// <summary>Tipo de cadastro que utiliza o CPF como chave.<summary>
        PessoaFisica = 1
    }
}