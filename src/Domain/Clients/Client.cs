namespace Agencia.Bancaria.Plataforma.Domain.Clients
{

    /// <summary>Entidade que representa um cliente agência bancária.</summary>
    public class Client
    {
        /// <summary>Código de identificação de um cliente.</summary>
        public string Id { get; }

        /// <summary>Nome do proprietário da conta.</summary>
        public string Nome { get; }
        
        /// <summary>Email do cliente proprietário da conta.</summary>
        public string Email { get; }

        /// <summary>Tipo de cliente.</summary>
        public ClientType TipoDeCliente { get; }
    
        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="id">Código de identificação do cliente.</param>
        /// <param name="nome">Nome do cliente.</param>
        /// <param name="email">Email do cliente para contato.</param>
        /// <param name="tipoDeCliente">Tipo de cliente.</param>
        public Client(string id ,string nome, string email, ClientType tipoDeCliente)
        {
            Id = id;
            Nome = nome;
            Email = email;
            TipoDeCliente = tipoDeCliente;
        }
    }  
}