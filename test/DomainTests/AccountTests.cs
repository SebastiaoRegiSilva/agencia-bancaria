using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Agencia.Plataforma.Domain.Accounts;
using Agencia.Plataforma.Domain.Clients;
using Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Accounts;
using Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agencia.DomainTests
{
    /// <summary>Classe de testes para domínio de contas.</summary>
    public class AccountTests
    {
        /// <summary>Serviço que provê acesso aos dados e operações relacionadas aos clientes na plataforma.</summary>
        private static ClientService _clientService = null;

        /// <summary>Serviço que provê acesso aos dados e operações relacionadas as contas sa plataforma.</summary>
        private static AccountService _accountService = null;
        
        
        /// <summary>Serviço que provê acesso aos dados e oeprações relacionadas as contas na plataforma.</summary>
        [ClassInitialize]
        public static void PrepareTestsData(TestContext context)
        {
            var ptBR = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = ptBR;
            Thread.CurrentThread.CurrentUICulture = ptBR;
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            string conString = config.GetValue<string>("DB:MongoDB:ConString");
            string database = config.GetValue<string>("DB:MongoDB:Database");

            var clientRepository = new ClientRepository(conString, database);
            _clientService = new ClientService(clientRepository);

            var accountRepository = new AccountRepository(conString, database);
            _accountService = new AccountService(accountRepository, _clientService);                
        }

        [TestMethod]
        public async Task CadastrarContaAsync()
        {
            int numeroConta = 0;
            string idCliente = "61fd4ecce91f7bb014ecbff4";
            AccountType tipoDaConta = AccountType.Corrente;
            DateTime dataCadastro = DateTime.Now;
            DateTime dataUltimoAcesso = new DateTime();
            DateTime dataAlteracao = DateTime.Now;
            decimal saldo = 0;
            AccountStatus statusDaConta = AccountStatus.Bloqueada;

        
            await _accountService.CadastrarContaAsync(numeroConta, idCliente, tipoDaConta, dataCadastro, dataUltimoAcesso, dataAlteracao, saldo, statusDaConta);
                        
        }

    }
}