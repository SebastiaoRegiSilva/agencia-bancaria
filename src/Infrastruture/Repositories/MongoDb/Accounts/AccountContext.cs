using Agencia.Bancaria.Plataforma.Infrastructure.Repositories.MongoDb.Accounts.Models;
using MongoDB.Driver;

namespace Agencia.Bancaria.Plataforma.Infrastructure.Repositories.MongoDb.Accounts
{
    /// <summary>Contexto utilizado para acesso aos dados das contas.</summary>
    class AccountContext
    {
        /// <summary>Base de dados MongoDB onde estão armazenados as contas.</summary>
        private readonly IMongoDatabase _database = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados MongoDB.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public AccountContext(string conString, string database)
        {
            var client = new MongoClient(conString);
            if (client != null)
                _database = client.GetDatabase(database);

            BsonMapConfig.Config();
        }

        /// <summary>Coleção de contas.</summary>
        public IMongoCollection<AccountModel> Contas
        {
            get
            {
                return _database.GetCollection<AccountModel>("Contas");
            }
        }
    }
}