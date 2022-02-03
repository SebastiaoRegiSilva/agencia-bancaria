using Agencia.Plataforma.Domain.Accounts;
using Agencia.Plataforma.Domain.Clients;
using Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Accounts;
using Agencia.Plataforma.Infrastructure.Repositories.MongoDb.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Agencia.DomainTests
{
    /// <summary>Classe de testes para o domínio de clientes.</summary>
    [TestClass]
    public class ClientTests
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
        public async Task CadastrarClienteAsync()
        {
            string nome = "Bruno Vitor Enzo Dias";
            string email = "bruno@teste.com";
            var tipoCliente = ClientType.PessoaJuridica;
        
            string idCliente = await _clientService.CadastrarClienteAsync(nome, email, tipoCliente);
            
            // Verifica se os valores foram armazenados corretamente no repositório.
            var clienteDb = await _clientService.BuscarClientePorIdAsync(idCliente);
            
            
            Assert.AreEqual(nome, clienteDb.Nome );
            Assert.AreEqual(email, clienteDb.Email );
            Assert.AreEqual(tipoCliente, clienteDb.TipoDeCliente);
        }

        [TestMethod]
        public async Task EditarClienteAsync()
        {
            // Cadastrar um cliente.
            string nome = "Rafael de Jesus Crisóstomo";
            string email = "rafacristo@zuckalenght.com";
            var tipoCliente = ClientType.PessoaFisica;

            string idCliente = await _clientService.CadastrarClienteAsync(nome, email, tipoCliente);
            
            // Editar o email e o tipo de conta.            
            email = "rafaelcrisostomojesus@zuckalenght.com";
            tipoCliente = ClientType.PessoaJuridica;

            // Editar cliente.
            await _clientService.EditarClienteAsync(idCliente, nome, email, tipoCliente);
            
            // Verifica se os valores foram armazenados corretamente no repositório.
            var clienteDb = await _clientService.BuscarClientePorIdAsync(idCliente);
            
            
            Assert.AreEqual(nome, clienteDb.Nome );
            Assert.AreEqual(email, clienteDb.Email );
            Assert.AreEqual(tipoCliente, clienteDb.TipoDeCliente);
        }

         [TestMethod]
        public async Task EditarClientePorNomeAsync()
        {
            // Cadastrar um cliente.
            string nome = "André Mattos de Oliveira";
            string email = "andre@motiva.com";
            var tipoCliente = ClientType.PessoaFisica;

            string idCliente = await _clientService.CadastrarClienteAsync(nome, email, tipoCliente);
            
            // Editar o email e o tipo de conta. 
            nome = "André Mattos";            
            email = "andredeoliceira@motiva.com";
            tipoCliente = ClientType.PessoaJuridica;

            // Editar cliente.
            await _clientService.EditarClienteAsync(idCliente, nome, email, tipoCliente);
            
            // Verifica se os valores foram armazenados corretamente no repositório.
            var clienteDb = await _clientService.BuscarClientePorNomeAsync(nome);
                        
            Assert.AreEqual(nome, clienteDb.Nome );
            Assert.AreEqual(email, clienteDb.Email );
            Assert.AreEqual(tipoCliente, clienteDb.TipoDeCliente);
        }

    }
}
