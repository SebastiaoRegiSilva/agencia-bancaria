namespace Agencia.Plataforma.Domain.Clients.Exceptions
{
    /// <summary> Exceção de negócio lançada na tentativa de atribuição de um endereço de e-mail inválido para uma entidade ou objeto de valor.</summary>
    public class AddressEmailInvalidException : BusinessException
    {
        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="invalidAddress">Endereço de e-mail inválido.</param>
        public AddressEmailInvalidException(string invalidAddress) :
            base("O endereço de email " + invalidAddress + " está incorreto.") {}
    }
}