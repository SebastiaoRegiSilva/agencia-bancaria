using Agencia.Plataforma.Domain.Clients;
using System;
using System.Threading.Tasks;

namespace Agencia.Plataforma.Domain.Accounts
{
    /// <summary>Serviço que provê aos dados das contas dos clientes.</summary>
    public class AccountService
    {
        /// <summary>Repositório para armazenamento das contas.</summary>
        private readonly IAccountRepository _accountRep;
        
        /// <summary>Serviço que provê acesso aos dados e operaçãoes dos clientes.</summary>
        private readonly ClientService _clientService;

        /// <summary> Construtor com injeção de dependência.</summary>
        /// <param name="accountRep">Repositório para armazenamento das contas.</param>
        /// <param name="clientRep">Serviço que provê acesso aos dados e operaçãoes dos clientes.</param>
        public AccountService(IAccountRepository accountRep, ClientService clientService)
        {
            _accountRep = accountRep;
            _clientService = clientService;
        }
        
        /// <summary>Recuperar no repositório a conta com base em seu número.</summary>
        /// <param name="numeroConta">Número da conta que se deseja recuperar.</param>
        /// <returns>Conta recuperada com base no número.</returns>
        public async Task<Account> BuscarContaPorNumeroAsync(int numeroConta)
        {
            return await _accountRep.RecuperarContaPorNumeroAsync(numeroConta);
        }

        /// <summary>Recuperar no repositório recuperar a conta com base em seu id.</summary>
        /// <param name="id">Código de identificação da conta.</param>
        /// <returns>Conta recuperada com base no código de identificação.</returns>
        public async Task<Account> BuscarContaPorIdAsync(string id)
        {
            return await _accountRep.RecuperarContaPorIdAsync(id);
        }            

        /// <summary>Cadastra no repositório uma nova conta no sistema.</summary>
        /// <param name="numeroConta">Número da conta do cliente.</param>
        /// <param name="idcliente">Código de ideintificação.</param>
        /// <param name="tipoDaConta">Tipo da conta.</param>
        /// <param name="dataCadastro">Data em que o cliente foi cadastrado no sistema.</param>
        /// <param name="dataUltimoAcesso">Data em que o cliente fez o último acesso no sistema.</param>
        /// <param name="dataAlteracao">Data em que foram feitas eventuais alterações cadastrais na conta.</param>
        /// <param name="saldo">Quantidade de saldo em conta.</param>
        /// <param name="statusDaConta">Situação da conta do cliente.</param>
        /// <returns>Código de identificação gerado para a conta cadastrada.</returns>
        public async Task <string> CadastrarContaAsync(string idCliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, 
        DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {
            
            var clienteRecuperado = await _clientService.BuscarClientePorIdAsync(idCliente);
            
            string idConta = await _accountRep.CadastrarContaAsync(clienteRecuperado, tipoDaConta, dataCadastro, dataUltimoAcesso, dataAlteracao, saldo, statusDaConta);

            return idConta;
        }
        
        /// <summary>Edita no repositório uma conta cadastrada no sistema.</summary>
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
            // Registra alteração na conta do cliente.
            dataAlteracao = DateTime.Now;
            
            await _accountRep.EditarContaAsync(id, numeroConta, cliente, tipoDaConta, dataCadastro, dataUltimoAcesso, dataAlteracao, saldo, statusDaConta);
        }
        
        /// <summary>Deleta no repositório uma conta e o cliente vinculado a essa conta.</summary>
        /// <param name="numeroConta">Número da conta que se deseja recuperar.</param>
        public async Task ExcluirContaAsync(int numeroConta)
        {
            await _accountRep.ExcluirContaAsync(numeroConta);
        }

        // Operações com a conta.
        
        /// <summary>Depositar saldo na conta do cliente.</summary>
        /// <param name="accountNumber">Número da conta do cliente.</param>
        /// <param name="value">Valor em reais a ser depositado na conta do cliente.</param>
        public async Task DepositarAsync(int accountNumber, decimal value)
        {    
            await _accountRep.DepositarContaAsync(accountNumber, value);

        }
    
    }
}