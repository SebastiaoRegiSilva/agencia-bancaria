using Agencia.Plataforma.Domain.Accounts;
using Agencia.Plataforma.Domain.Clients;
using Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Accounts;
using Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Agencia.DomainTests
{
    /// <summary>Classe de testes para domínio de contas.</summary>
    [TestClass]
    public class AccountTests
    {
        /// <summary>Serviço que provê acesso aos dados e operações relacionadas aos clientes na plataforma.</summary>
        private static ClientService _clientService = null;

        /// <summary>Serviço que provê acesso aos dados e operações relacionadas as contas sa plataforma.</summary>
        private static AccountService _accountService = null;
        
        /// <summary>Método de preparação para os testes.</summary>
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
            // Cadastrar um cliente para recuperar o código de identificação para cadastro em um nova conta.
            string nome = "André Mattos de Oliveira";
            string email = "andre@motiva.com";
            var tipoCliente = ClientType.PessoaFisica;

            // Cadastrar uma nova conta.
            int numeroConta = 0;
            string idCliente = await _clientService.CadastrarClienteAsync(nome, email, tipoCliente);
            AccountType tipoDaConta = AccountType.Corrente;
            DateTime dataCadastro = DateTime.UtcNow;
            DateTime dataUltimoAcesso = new DateTime();
            DateTime dataAlteracao = DateTime.UtcNow;
            decimal saldo = 0;
            AccountStatus statusDaConta = AccountStatus.Bloqueada;

        
            var idConta = await _accountService.CadastrarContaAsync(numeroConta, idCliente, tipoDaConta, dataCadastro, dataUltimoAcesso, dataAlteracao, saldo, statusDaConta);
        
            // Verifica se os valores foram armazenados corretamente no repositório.
            var accountDb = await _accountService.BuscarContaPorIdAsync(idConta);
                        
            Assert.AreEqual(numeroConta, 0);
            Assert.AreEqual(idCliente, accountDb.Cliente.Id);
            Assert.AreEqual(tipoDaConta, accountDb.Tipo);
            /* Assert.AreEqual<DateTime>(dataCadastro, accountDb.DataCadastro);
            Assert.AreEqual(dataUltimoAcesso, accountDb.DataUltimoAcesso);        VERIFICAR A QUESTÃO DAS DATAS.
            Assert.AreEqual(dataAlteracao, accountDb.DataAlteracao.Value);*/
            Assert.AreEqual(saldo, accountDb.Saldo);
            Assert.AreEqual(statusDaConta, accountDb.Status);

        }


        [TestMethod]
        public async Task EditarContaAsync()
        {
            // Cadastrar um cliente para recuperar o código de identificação para cadastro em um nova conta.
            string nome = "Luan Lucas de Sá Magalhães";
            string email = "luanlucas@relier.com.br";
            var tipoCliente = ClientType.PessoaFisica;

            // Cadastrar uma nova conta.
            int numeroConta = 1;
            string idCliente = await _clientService.CadastrarClienteAsync(nome, email, tipoCliente);
            Client clienteRecuperado = await _clientService.BuscarClientePorIdAsync(idCliente);
            AccountType tipoDaConta = AccountType.Poupanca;
            DateTime dataCadastro = DateTime.UtcNow;
            DateTime dataUltimoAcesso = new DateTime();
            DateTime dataAlteracao = DateTime.UtcNow;
            decimal saldo = 0;
            AccountStatus statusDaConta = AccountStatus.Bloqueada;

        
            var idConta = await _accountService.CadastrarContaAsync(numeroConta, idCliente, tipoDaConta, dataCadastro, dataUltimoAcesso, dataAlteracao, saldo, statusDaConta);

            // Editar características da conta.       
            numeroConta = 15;
            tipoDaConta = AccountType.Poupanca;
            dataCadastro = DateTime.UtcNow;
            dataUltimoAcesso = new DateTime();
            dataAlteracao = DateTime.UtcNow;
            saldo = 154.14M;
            statusDaConta = AccountStatus.Ativa;
            
            await _accountService.EditarContaAsync(idConta, numeroConta, clienteRecuperado, tipoDaConta, dataCadastro, dataUltimoAcesso, dataAlteracao, saldo, statusDaConta);
            
            // Verifica se os valores foram armazenados corretamente no repositório.
            var accountDb = await _accountService.BuscarContaPorIdAsync(idConta);
                        
            Assert.AreEqual(numeroConta, 15);
            Assert.AreEqual(idCliente, accountDb.Cliente.Id);
            Assert.AreEqual(tipoDaConta, accountDb.Tipo);
            /* Assert.AreEqual<DateTime>(dataCadastro, accountDb.DataCadastro);
            Assert.AreEqual(dataUltimoAcesso, accountDb.DataUltimoAcesso);        VERIFICAR A QUESTÃO DAS DATAS.
            Assert.AreEqual(dataAlteracao, accountDb.DataAlteracao.Value);*/
            Assert.AreEqual(saldo, accountDb.Saldo);
            Assert.AreEqual(statusDaConta, accountDb.Status);
        }

        [TestMethod]
        public async Task DepositarContaAsync()
        {
            // Cadastrar um cliente para recuperar o código de identificação para cadastro em um nova conta.
            string nome = "Vera de Souza Aguiar";
            string email = "veradesouzaaguiar@reobot.com";
            var tipoCliente = ClientType.PessoaJuridica;

            // Cadastrar uma nova conta.
            int numeroConta = 7;
            string idCliente = await _clientService.CadastrarClienteAsync(nome, email, tipoCliente);
            Client clienteRecuperado = await _clientService.BuscarClientePorIdAsync(idCliente);
            AccountType tipoDaConta = AccountType.Poupanca;
            DateTime dataCadastro = DateTime.UtcNow;
            DateTime dataUltimoAcesso = new DateTime();
            DateTime dataAlteracao = DateTime.UtcNow;
            decimal saldo = 0;
            AccountStatus statusDaConta = AccountStatus.Bloqueada;

        
            var idConta = await _accountService.CadastrarContaAsync(numeroConta, idCliente, tipoDaConta, dataCadastro, dataUltimoAcesso, dataAlteracao, saldo, statusDaConta);

            // Realizar depósitos na conta do cliente cadastrado.
            await _accountService.DepositarAsync(numeroConta, 174.41M);       
            

            // Verifica se os valores foram armazenados corretamente no repositório.
            var accountDb = await _accountService.BuscarContaPorIdAsync(idConta);
                        
            Assert.AreEqual(numeroConta, 7);
            Assert.AreEqual(idCliente, accountDb.Cliente.Id);
            Assert.AreEqual(tipoDaConta, accountDb.Tipo);
            /* Assert.AreEqual<DateTime>(dataCadastro, accountDb.DataCadastro);
            Assert.AreEqual(dataUltimoAcesso, accountDb.DataUltimoAcesso);        VERIFICAR A QUESTÃO DAS DATAS.
            Assert.AreEqual(dataAlteracao, accountDb.DataAlteracao.Value);*/
            Assert.AreEqual(saldo, 174.41M);
            Assert.AreEqual(statusDaConta, accountDb.Status);
        }

    }
}