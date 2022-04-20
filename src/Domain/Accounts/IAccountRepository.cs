using Agencia.Plataforma.Domain.Clients;
using System;
using System.Threading.Tasks;

namespace Agencia.Plataforma.Domain.Accounts
{
    /// <summary>Interface que padroniza o repositório das contas.</summary>
    public interface IAccountRepository
    {
        /// <summary>Cadastra na base de dados uma nova conta no sistema.</summary>
        /// <param name="cliente">Proprietário da conta.</param>
        /// <param name="tipoDaConta">Tipo da conta.</param>
        /// <param name="dataCadastro">Data em que o cliente foi cadastrado no sistema.</param>
        /// <param name="dataUltimoAcesso">Data em que o cliente fez o último acesso no sistema.</param>
        /// <param name="dataAlteracao">Data em que foram feitas eventuais alterações cadastrais na conta.</param>
        /// <param name="saldo">Quantidade de saldo em conta.</param>
        /// <param name="statusDaConta">Situação da conta do cliente.</param>
        /// <returns>Código de identificação gerado para a conta cadastrada.</returns>
        Task<string> CadastrarContaAsync(Client cliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, 
        DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta);
        
        /// <summary>Recupera na base de dados uma conta com base em seu número.</summary>
        /// <param name="numeroConta"> Número da conta.</param>
        /// <returns>Objeto de valor contendo as informações da conta recuperada.</returns>
        Task<Account> RecuperarContaPorNumeroAsync(int numeroConta);

        /// <summary>Recupera na base de dados uma conta com base em seu número.</summary>
        /// <param name="id"> Código de identificação da conta.</param>
        /// <returns>Objeto de valor contendo as informações da conta recuperada.</returns>
        Task<Account> RecuperarContaPorIdAsync(string id);

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
        Task EditarContaAsync(string id, int numeroConta, Client cliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, 
        DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta);
        
        /// <summary>Exclui na base de dados uma conta cadastrada no sistema.</summary>
        /// <param name="numeroConta">Número da conta.</param>
        Task ExcluirContaAsync(int numeroConta);

        /// <summary>Realiza depósito em conta com base no númera do conta.</summary>
        /// <param name="numeroConta">Número da conta.</param>
        /// <param name="valor">Valor a ser depositado na conta.</param>
        Task DepositarContaAsync(int numeroConta, decimal valor);

        /// <summary>Recuperar na base de dados o saldo em conta de um cliente cadastrado.</summary>
        /// <param name="numeroConta">Número da conta.</param>
        ///<returns>Saldo em conta.</returns>
        Task<decimal> RecuperarSaldoAsync(int numeroConta);

        /// <summary>Sacar na conta do cliente.</summary>
        /// <param name="accountNumber">Número da conta do cliente.</param>
        /// <param name="value">Valor em reais a ser sacado na conta do cliente.</param>
        Task SacarContaAsync(int accountNumber, decimal value);
    }
}
