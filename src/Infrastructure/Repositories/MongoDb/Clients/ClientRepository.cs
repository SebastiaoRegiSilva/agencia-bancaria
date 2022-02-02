using Agencia.Plataforma.Domain.Clients;
using Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Clients.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Clients
{
    /// <summary>Implementação do repositório de clientes para o Mongo DB.</summary>
    public class ClientRepository : IClientRepository
    {
        /// <summary>Contexto utilizado pelo repositório de clientes para acessar a coleção de cliente na base de dados.</summary> 
        private readonly ClientContext _ctxClient = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public ClientRepository(string conString, string database)
        {
            _ctxClient = new ClientContext(conString, database);
        }

        /// <summary>Cadastra na base de dados um novo cliente no sistema.</summary>
        /// <param name="nome">Nome do cliente.</param>
        /// <param name="email">Email do cliente para contato.</param>
        /// <param name="tipoDeCliente">Tipo de cliente.</param>
        /// <returns>Código de identificação gerado para o cliente cadastrado.</returns>
        public async Task<string> CadastrarClienteAsync(string nome, string email, ClientType tipoDeCliente)
        {
            var model = new ClientModel
            {
                Nome = nome,
                Email = email,
                TipoDeCliente = tipoDeCliente

            };

            await _ctxClient.Clientes.InsertOneAsync(model);

            return model.Id;
        }
        
        /// <summary>Recupera na base de dados um cliente com base em seu código de identificação.</summary>
        /// <param name="id">Código de identificação do cliente a ser recuperado.</param>
        /// <returns>Objeto de valor contendo as informações do cliente recuperado.</returns>
        public async Task<Client> RecuperarClientePorIdAsync(string id)
        {
            var builder = Builders<ClientModel>.Filter;
            var filter = builder.Eq(c => c.Id, id);
            
            return await _ctxClient.Clientes
                .Aggregate()
                .Match(filter)
                .FirstOrDefaultAsync();
        }

        /// <summary>Recupera na base de dados todos os clientes cadastrados.</summary>
        /// <returns>Todos os cliente cadastrados.</returns>
        // public async Task<List<Client>> RecuperarTodosAsync()
        // {
        //     // var builder = Builders<ClientModel>.Filter;

        //     // return await _ctxClient.Clientes
        //     //             .Find(s=>true)
        //     //             .ToListAsync();        
        // }

        /// <summary>Recupera na base de dados um cliente com base em seu código de identificação.</summary>
        /// <param name="nome">Nome do cliente a ser recuperado.</param>
        /// <returns>Objeto de valor contendo as informações do cliente recuperado.</returns>
        public async Task<Client> RecuperarClientePorNomeAsync(string nome)
        {
            var builder = Builders<ClientModel>.Filter;
            var filter = builder.Eq(c => c.Nome, nome);
            
            return await _ctxClient.Clientes
                .Aggregate()
                .Match(filter)
                .FirstOrDefaultAsync();
        }

        /// <summary>Edita na base de dados o cliente cadastrado no sistema.</summary>
        /// <param name="id">Código de identificação do cliente.</param>
        /// <param name="nome">Nome do cliente.</param>
        /// <param name="email">Email do cliente para contato.</param>
        /// <param name="tipoDeCliente">Tipo de cliente.</param>
        public async Task EditarClientAsync(string id ,string nome, string email, ClientType tipoDeCliente)
        {
            var builder = Builders<ClientModel>.Filter;
            var filter = builder.Eq(c => c.Id, id);

            var update = Builders<ClientModel>.Update
                .Set(c => c.Nome, nome)
                .Set(c => c.Email, email)
                .Set(c => c.TipoDeCliente, tipoDeCliente);

            await _ctxClient.Clientes.UpdateOneAsync(filter, update);
        }

        /// <summary>Exclui na base de dados o cliente cadastrado no sistema.</summary>
        /// <param name="id">Código de identificação do cliente.</param>
        public async Task ExcluirClienteAsync(string id)
        {
            var filter = Builders<ClientModel>.Filter.Eq(c => c.Id, id);

            await _ctxClient.Clientes.DeleteOneAsync(filter);
        }    
    }
}