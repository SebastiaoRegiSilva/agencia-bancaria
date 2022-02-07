namespace Agencia.Plataforma.Infrastructure.Repositories.Local.Clients
{
    public class ClientRepository : IClientRepository
    {
        /// <summary>String de conexão para a base de dados MySQL.</summary>
        string _conString = string.Empty;

        /// <summary>Construtor padrão.</summary>
        /// <param name="connectionString">String de conexão para a base de dados MySQL.</param>
        public ClientRepository(string connectionString)
        {
            _conString = connectionString;
        }

        /// <summary>CUIDADO! Remove todos os clientes do repositório.</summary>
        public void LimparTudo()
        {
            var cmd = @"delete from client";

            MySqlHelper.ExecuteNonQuery(_conString, cmd);
        }
        Task<string> IClientRepository.CadastrarClienteAsync(string nome, string email, ClientType tipoDeCliente)
        {
            throw new System.NotImplementedException();
        }

        Task IClientRepository.EditarClientAsync(string id, string nome, string email, ClientType tipoDeCliente)
        {
            throw new System.NotImplementedException();
        }

        Task IClientRepository.ExcluirClienteAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        Task<Client> IClientRepository.RecuperarClientePorIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        Task<Client> IClientRepository.RecuperarClientePorNomeAsync(string nome)
        {
            throw new System.NotImplementedException();
        }
    }
}