using Agencia.Plataforma.Domain.Accounts;
using Agencia.Plataforma.Domain.Clients;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Agencia.Plataforma.Infrastructure.Repositories.Local.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        public Task<string> CadastrarContaAsync(Client cliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {
            throw new NotImplementedException();
        }

        public Task DepositarContaAsync(int numeroConta, decimal valor)
        {
            throw new NotImplementedException();
        }

        public Task EditarContaAsync(string id, int numeroConta, Client cliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {
            throw new NotImplementedException();
        }

        public Task ExcluirContaAsync(int numeroConta)
        {
            throw new NotImplementedException();
        }

        public Task<Account> RecuperarContaPorIdAsync(string id)
        {
            throw new NotImplementedException(); 
        }

        public Task<Account> RecuperarContaPorNumeroAsync(int numeroConta)
        {
            throw new NotImplementedException();
        }

        Task<string> IAccountRepository.CadastrarContaAsync(Client cliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {
            throw new NotImplementedException();
        }

        Task IAccountRepository.DepositarContaAsync(int numeroConta, decimal valor)
        {
            throw new NotImplementedException();
        }

        Task IAccountRepository.EditarContaAsync(string id, int numeroConta, Client cliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {
            throw new NotImplementedException();
        }

        Task IAccountRepository.ExcluirContaAsync(int numeroConta)
        {
            throw new NotImplementedException();
        }

        Task<Account> IAccountRepository.RecuperarContaPorIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<Account> IAccountRepository.RecuperarContaPorNumeroAsync(int numeroConta)
        {
            throw new NotImplementedException();
        }

        Task<decimal> IAccountRepository.RecuperarSaldoAsync(int numeroConta)
        {
            throw new NotImplementedException();
        }

        Task IAccountRepository.SacarContaAsync(int accountNumber, decimal value)
        {
            throw new NotImplementedException();
        }

        // /// <summary>Caminho para pasta de cadastro das contas.</summary>
        // private void CriarDiretorio()
        // {
        //     string caminhoPasta = @"/home/relier/projetos/agencia-bancaria/src/Infrastructure/Repositories/Local";

        //     string pathString = Path.Combine(caminhoPasta, "Agência Bancária.");

        //     Directory.CreateDirectory(pathString);
        // }
    }
}