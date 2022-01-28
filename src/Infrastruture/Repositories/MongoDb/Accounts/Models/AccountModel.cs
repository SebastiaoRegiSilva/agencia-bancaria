using Agencia.Bancaria.Plataforma.Domain.Accounts;
using Agencia.Bancaria.Plataforma.Domain.Clients;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Agencia.Bancaria.Plataforma.Infrastructure.Repositories.MongoDb.Accounts.Models
{

    /// <summary>Modelo que representa uma conta na base de dados.</summary>
    public class AccountModel
    {
        /// <summary>Código de identificação da conta do cliente.</summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set;}

        /// <summary>Número da conta do cliente.</summary>
        public int NumeroConta { get; set;}
        
        /// <summary>Proprietário da conta.</summary>
        public Client Cliente { get; set;}
        
        /// <summary>Tipo da conta.</summary>
        public AccountType Tipo { get; set;}
        
        /// <summary>Data em que o cliente foi cadastrado no sistema.</summary>
        public DateTime DataCadastro { get; set;}

        /// <summary>Data em que o cliente fez o último acesso no sistema.</summary>
        public DateTime DataUltimoAcesso { get; set;}

        /// <summary>Data em que foram feitas eventuais alterações cadastrais na conta.</summary>
        public DateTime? DataAlteracao { get; set;}

        /// <summary>Quantidade de saldo em conta.</summary>
        public decimal Saldo { get; set;}

        /// <summary>Situação da conta do cliente.</summary>
        public AccountStatus Status { get; set; }    
    
        /// <summary>Converte uma conta no modelo do contexto Mongo para uma conta no domínio.</summary>
        /// <param name="accountModel">Conta no modelo do contexto Mongo.</param>
        public static implicit operator Account(AccountModel contaModel)
        {
            if (contaModel == null)
                return null;

            return new Account(
                contaModel.Id,
                contaModel.NumeroConta,
                contaModel.Cliente,
                contaModel.Tipo,
                contaModel.DataCadastro,
                contaModel.DataUltimoAcesso,
                contaModel.DataAlteracao.Value,
                contaModel.Saldo,
                contaModel.Status
            );
        }
    }  
}