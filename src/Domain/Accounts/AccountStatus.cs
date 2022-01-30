namespace Agencia.Plataforma.Domain.Accounts
{
    /// <summary>Possíveis situações da conta do cliente.</summary>    
    public enum AccountStatus
    {
        /// <summary>A conta do cliente está ativa.</summary>
        Ativa = 0,

        /// <summary>A conta do cliente foi bloqueada.</summary>
        Bloqueada = 1,

        /// <summary>A conta do cliente foi excluída.</summary>
        Excluida = 2,

        /// <summary>A conta do cliente foi desativada por ele mesmo.</summary>
        ContaDesativada = 3
    }
}