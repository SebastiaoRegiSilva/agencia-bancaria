using Agencia.Bancaria.Plataforma.Infrastructure.Repositories.MongoDb.Accounts.Models;
using Agencia.Bancaria.Plataforma.Infrastructure.Repositories.MongoDb.Clients.Models;
using MongoDB.Bson.Serialization;

namespace Agencia.Bancaria.Plataforma.Infrastructure.Repositories.MongoDb
{
    /// <summary>Classe de configuração responsável pelo mapeamento geral dos modelos na base de dados.</summary>
    public static class BsonMapConfig
    {
        /// <summary>Variável que verifica se o método já foi acessado por outro contexto.</summary>
        private static bool _hit = false;

        /// <summary>Método de mapeamento dos modelos.</summary>
        public static void Config()
        {
            if (_hit)
                return;

            BsonClassMap.RegisterClassMap<AccountModel>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<ClientModel>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });

            _hit = true;
        }
    }
}