using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agencia.Plataforma.Domain.Clients
{
    /// <summary>Serviço que provê acesso aos dados clientes.</summary>
    public class ClientService
    {
        /// <summary>Repositório para armazenamento dos clientes.</summary>
        private readonly IClientRepository _clientRep;

        /// <summary>Construtor com injeção de dependência.</summary>
        /// <param name="clientRep">Repositório para armazenamento de clientes.</param>
        public ClientService(IClientRepository clientRep)
        {
            _clientRep = clientRep;
        }

        /// <summary>Cadastra no repositório um novo cliente no sistema.</summary>
        /// <param name="nome">Nome do cliente.</param>
        /// <param name="email">Email do cliente para contato.</param>
        /// <param name="tipoDeCliente">Tipo de cliente.</param>
        /// <returns>Código de identificação gerado para o cliente cadastrado.</returns>
        public async Task <string> CadastrarClienteAsync(string nome, string email, ClientType tipoDeCliente)
        {
            string idCliente =  await _clientRep.CadastrarClienteAsync(nome, email, tipoDeCliente);

            return idCliente;
        }

        /// <summary>Recuperar no repositório um cliente com base no seu código de identificação.</summary>
        /// <param name="id">Código de identificação do cliente que se deseja recuperar.</param>
        /// <returns>Cliente recuperado.</returns>
        public async Task<Client> BuscarClientePorIdAsync(string id)
        {
            return await _clientRep.RecuperarClientePorIdAsync(id);
            
        }
        
        /// <summary>Recuperar no repositório um cliente com base no seu código de identificação.</summary>
        /// <param name="nome">Nome do cliente que se deseja recuperar.</param>
        /// <returns>Cliente recuperado.</returns>
        public async Task<Client> BuscarClientePorNomeAsync(string nome)
        {
            return await _clientRep.RecuperarClientePorNomeAsync(nome);
            
        }

        /// <summary>Recuperar no repositório os clientes cadastrados.</summary>
        /// <returns>Todos os clientes cadastrados.</returns>
       // public async Task<List<Client>> BuscarTodosAsync()
        // {
        //     // return await _clientRep.RecuperarTodosAsync();
            
        // }


        /// <summary>Edita no repositório um cliente.</summary>
        /// <param name="id">Código de identificação do cliente.</param>
        /// <param name="nome">Nome do cliente.</param>
        /// <param name="email">Email do cliente para contato.</param>
        /// <param name="tipoDeCliente">Tipo de cliente.</param>
        public async Task EditarClienteAsync(string id ,string nome, string email, ClientType tipoDeCliente)
        {
            await _clientRep.EditarClientAsync(id, nome, email, tipoDeCliente);
        }
    
        /// <summary>Exclui no repositório um cliente.</summary>
        /// <param name="id">Código de identificação do cliente.</param>
        public async Task DeletarClienteAsync(string id)
        {
            await _clientRep.ExcluirClienteAsync(id);
        }
    }
}