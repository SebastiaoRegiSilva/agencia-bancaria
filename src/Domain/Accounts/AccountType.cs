namespace Agencia.Bancaria.Plataforma.Domain.Accounts
{
    /// <summary>Possíveis tipos de conta para o cliente.</summary>
    public enum AccountType
    {
        /// <summary>A conta do cliente é salário.</summary>
        Salario = 0,
        
        /// <summary>A conta do cliente é corrente.</summary>
        Corrente = 1,
        
        /// <summary>A conta do cliente é poupança.</summary>
        Poupanca = 2,
        
        /// <summary>A conta do cliente é digital.</summary>
        Digital = 3,
        
        /// <summary>A conta do cliente é universitária.</summary>
        Universitaria = 4
    }
}