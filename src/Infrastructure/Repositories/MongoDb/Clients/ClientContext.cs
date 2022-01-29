using Agencia.Bancaria.Plataforma.Infrastructure.Repositories.MongoDb.Clients.Models;
using MongoDB.Driver;

namespace Agencia.Bancaria.Plataforma.Infrastructure.Repositories.MongoDb.Clients
{
    /// <summary>Contexto utilizado para acesso aos dados dos clientes.</summary>
    class ClientContext
    {
        /// <summary>Base de dados MongoDB onde estão armazenados os usuários.</summary>
        private readonly IMongoDatabase _database = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados MongoDB.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public ClientContext(string conString, string database)
        {
            var client = new MongoClient(conString);
            if (client != null)
                _database = client.GetDatabase(database);

            BsonMapConfig.Config();
        }

        /// <summary>Coleção de clientes.</summary>
        public IMongoCollection<ClientModel> Clientes
        {
            get
            {
                return _database.GetCollection<ClientModel>("Clientes");
            }
        }
    }
}