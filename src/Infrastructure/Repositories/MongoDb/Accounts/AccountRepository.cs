using Agencia.Plataforma.Domain.Accounts;
using Agencia.Plataforma.Domain.Clients;
using Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Accounts.Models;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Accounts
{
    /// <summary>Implementação do repositório de contas para o Mongo DB.</summary>
    public class AccountRepository : IAccountRepository
    {
        /// <summary>Contexto utilizado pelo repositório de contas para acessar a coleção de uma conta na base de dados.</summary> 
        private readonly AccountContext _ctxAccount = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public AccountRepository(string conString, string database)
        {
            _ctxAccount = new AccountContext(conString, database);
        }

        /// <summary>Cadastra na base de dados uma nova conta no sistema.</summary>
        /// <param name="id">Código de identificação da conta.</param>
        /// <param name="numeroConta">Número da conta do cliente.</param>
        /// <param name="cliente">Proprietário da conta.</param>
        /// <param name="tipoDaConta">Tipo da conta.</param>
        /// <param name="dataCadastro">Data em que o cliente foi cadastrado no sistema.</param>
        /// <param name="dataUltimoAcesso">Data em que o cliente fez o último acesso no sistema.</param>
        /// <param name="dataAlteracao">Data em que foram feitas eventuais alterações cadastrais na conta.</param>
        /// <param name="saldo">Quantidade de saldo em conta.</param>
        /// <param name="statusDaConta">Situação da conta do cliente.</param>
        /// <returns>Código de identificação gerado para a conta cadastrada.</returns>
        public async Task<string> CadastrarContaAsync(Client cliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, 
        DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {
            // TODO: Mecanismo para impedir duas contas com o mesmo número.
            
            int numeroConta = Account.GerarNumeroConta();

            var model = new AccountModel
            {
                NumeroConta = numeroConta,
                Cliente = cliente,
                Tipo = tipoDaConta,
                DataCadastro = dataCadastro,
                DataUltimoAcesso = dataUltimoAcesso,
                DataAlteracao = dataAlteracao ,
                Saldo = saldo,
                Status = statusDaConta
                
            };

            await _ctxAccount.Contas.InsertOneAsync(model);

            return model.Id;
        }
        
        /// <summary>Recupera na base de dados uma conta com base em seu número.</summary>
        /// <param name="numeroConta"> Número da conta.</param>
        /// <returns>Objeto de valor contendo as informações da conta recuperada.</returns>
        public async Task<Account> RecuperarContaPorNumeroAsync(int numeroConta)
        {
            var builder = Builders<AccountModel>.Filter;
            var filter = builder.Eq(a => a.NumeroConta, numeroConta);
            
            return await _ctxAccount.Contas
                .Aggregate()
                .Match(filter)
                .FirstOrDefaultAsync();
        }

        /// <summary>Recupera na base de dados uma conta com base em seu número.</summary>
        /// <param name="id">Código de identificação da conta.</param>
        /// <returns>Objeto de valor contendo as informações da conta recuperada.</returns>
        public async Task<Account> RecuperarContaPorIdAsync(string id)
        {
            var builder = Builders<AccountModel>.Filter;
            var filter = builder.Eq(a => a.Id, id);
            
            return await _ctxAccount.Contas
                .Aggregate()
                .Match(filter)
                .FirstOrDefaultAsync();
        }


        /// <summary>Edita na base de dados uma conta cadastrada no sistema.</summary>
        /// <param name="id">Código de identificação da conta.</param>
        /// <param name="numeroConta">Número da conta do cliente.</param>
        /// <param name="cliente">Proprietário da conta.</param>
        /// <param name="tipoDaConta">Tipo da conta.</param>
        /// <param name="dataCadastro">Data em que o cliente foi cadastrado no sistema.</param>
        /// <param name="dataUltimoAcesso">Data em que o cliente fez o último acesso no sistema.</param>
        /// <param name="dataAlteracao">Data em que foram feitas eventuais alterações cadastrais na conta.</param>
        /// <param name="saldo">Quantidade de saldo em conta.</param>
        /// <param name="statusDaConta">Situação da conta do cliente.</param>
        public async Task EditarContaAsync(string id, int numeroConta, Client cliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, 
        DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {
            var builder = Builders<AccountModel>.Filter;
            var filter = builder.Eq(a => a.Id, id);

            var update = Builders<AccountModel>.Update
                .Set(a => a.Id, id)
                .Set(a => a.NumeroConta, numeroConta)
                .Set(a => a.Cliente, cliente)
                .Set(a => a.Tipo, tipoDaConta)
                .Set(a => a.DataCadastro, dataCadastro)
                .Set(a => a.DataUltimoAcesso, dataUltimoAcesso)
                .Set(a => a.DataAlteracao, dataAlteracao)
                .Set(a => a.Saldo, saldo)
                .Set(a => a.Status, statusDaConta);

            await _ctxAccount.Contas.UpdateOneAsync(filter, update);
        }

        /// <summary>Exclui na base de dados uma conta cadastrada no sistema.</summary>
        /// <param name="numeroConta">Número da conta.</param>
        public async Task ExcluirContaAsync(int numeroConta)
        {
            var filter = Builders<AccountModel>.Filter.Eq(a => a.NumeroConta, numeroConta);

            await _ctxAccount.Contas.DeleteOneAsync(filter);
        }

        /// <summary>Realiza depósito em conta com base no númera do conta.</summary>
        /// <param name="numeroConta">Número da conta.</param>
        /// <param name="valor">Valor a ser depositado na conta.</param>
        public async Task DepositarContaAsync(int numeroConta, decimal valor)
        {
            var filter = Builders<AccountModel>.Filter.Eq(a => a.NumeroConta, numeroConta);
            var update = Builders<AccountModel>.Update
                .Set(a => a.Saldo, valor)
                .Set(a => a.DataUltimoAcesso, DateTime.UtcNow);

            await _ctxAccount.Contas.UpdateOneAsync(filter, update);
        }
    }
}