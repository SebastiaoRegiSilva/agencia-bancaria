namespace Agencia.Plataforma.Domain.Clients.Exceptions
{
    /// <summary>Exceção da regra de negócio.</summary>
    public class BusinessException : System.Exception
    {
        /// <summary>Dados relacionados a exceção, úteis para o retorno.</summary>
        public object Dados { get; protected set; }

        /// <summary>Construtor padrão.</summary>
        /// <param name="mensagem">Mensagem com maiores informações da exceção gerada pela regra de negócio.</param>
        /// <returns>Conteúdo do erro.</returns>
        public BusinessException(string mensagem) :
            base(mensagem) {}
    }
}