using Agencia.Bancaria.Plataforma.Domain.Clients;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Agencia.Bancaria.Plataforma.Infrastructure.Repositories.MongoDb.Clients.Models
{
    /// <summary>Modelo que representa um cliente na base de dados.</summary>
    public class ClientModel
    {
        /// <summary>Código de identificação da conta do cliente.</summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>Nome do proprietário da conta.</summary>
        public string Nome { get; set; }
        
        /// <summary>Email do cliente proprietário da conta.</summary>
        public string Email { get; set; }

        /// <summary>Tipo de cliente.</summary>
        public ClientType TipoDeCliente { get; set; }

        /// <summary>Converte um cliente no modelo do contexto Mongo para uma conta no domínio.</summary>
        /// <param name="clientModel">Cliente no modelo do contexto Mongo.</param>
        public static implicit operator Client(ClientModel clientModel)
        {
            if (clientModel == null)
                return null;

            return new Client(
                clientModel.Id,
                clientModel.Nome,
                clientModel.Email,
                clientModel.TipoDeCliente               
            );
        }
    }  
}