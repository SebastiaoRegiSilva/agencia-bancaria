using System.Threading.Tasks;

namespace Agencia.Plataforma.Domain.Clients
{
    /// <summary>Interface que padroniza o repositório dos clientes.</summary>
    public interface IClientRepository
    {
        /// <summary>Cadastra na base de dados um novo cliente no sistema.</summary>
        /// <param name="nome">Nome do cliente.</param>
        /// <param name="email">Email do cliente para contato.</param>
        /// <param name="tipoDeCliente">Tipo de cliente.</param>
        /// <returns>Código de identificação gerado para o cliente cadastrado.</returns>
        Task<string> CadastrarClienteAsync(string nome, string email, ClientType tipoDeCliente);
        
        /// <summary>Recuperar no repositório um cliente com base no seu código de identificação.</summary>
        /// <param name="nome">Nome do cliente que se deseja recuperar.</param>
        /// <returns>Cliente recuperado.</returns>
        Task<Client> RecuperarClientePorNomeAsync(string nome);
        
        /// <summary>Recupera na base de dados um cliente com base em seu código de identificação.</summary>
        /// <param name="id">Código de identificação do cliente a ser recuperado.</param>
        /// <returns>Objeto de valor contendo as informações do cliente recuperado.</returns>
        Task<Client> RecuperarClientePorIdAsync(string id);
        
        /// <summary>Edita na base de dados o cliente cadastrado no sistema.</summary>
        /// <param name="id">Código de identificação do cliente.</param>
        /// <param name="nome">Nome do cliente.</param>
        /// <param name="email">Email do cliente para contato.</param>
        /// <param name="tipoDeCliente">Tipo de cliente.</param>
        Task EditarClientAsync(string id ,string nome, string email, ClientType tipoDeCliente);
        
        
        /// <summary>Exclui na base de dados o cliente cadastrado no sistema.</summary>
        /// <param name="id">Código de identificação do cliente.</param>
        Task ExcluirClienteAsync(string id);
    }
}
      