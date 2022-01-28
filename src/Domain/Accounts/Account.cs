using Agencia.Bancaria.Plataforma.Domain.Clients;
using System;

namespace Agencia.Bancaria.Plataforma.Domain.Accounts
{

    /// <summary>Entidade que representa uma conta na agência bancária.</summary>
    public class Account
    {
        /// <summary>Código de identificação da conta do cliente.</summary>
        public string Id { get; }
        
        /// <summary>Número da conta do cliente.</summary>
        public int NumeroConta { get; }
        
        /// <summary>Proprietário da conta.</summary>
        public Client Cliente { get; }
        
        /// <summary>Tipo da conta.</summary>
        public AccountType Tipo { get; }
        
        /// <summary>Data em que o cliente fez seu cadastro no sistema.</summary>
        public DateTime DataCadastro { get; }

        /// <summary>Data em que o cliente fez o último acesso no sistema.</summary>
        public DateTime DataUltimoAcesso { get; }

        /// <summary>Data em que foram feitas eventuais alterações cadastrais na conta.</summary>
        public DateTime? DataAlteracao { get; }

        /// <summary>Quantidade de saldo em conta.</summary>
        public decimal Saldo { get; }

        /// <summary>Situação da conta do cliente.</summary>
        public AccountStatus Status { get; }
    
        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="id">Código de identificação da conta.</param>
        /// <param name="numeroConta">Número da conta do cliente.</param>
        /// <param name="cliente">Proprietário da conta.</param>
        /// <param name="tipoDaConta">Tipo da conta.</param>
        /// <param name="dataCadastro">Data em que o cliente foi cadastrado no sistema.</param>
        /// <param name="dataUltimoAcesso">Data em que o cliente fez o último acesso no sistema.</param>
        /// <param name="dataAlteracao">Data em que foram feitas eventuais alterações cadastrais na conta.</param>
        /// <param name="saldo">Quantidade de saldo em conta.</param>
        /// <param name="statusDaConta">Situação da conta do cliente.</param>
        public Account(string id, int numeroConta, Client cliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, 
        DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {
            Id = id;
            NumeroConta = numeroConta;
            Cliente = cliente;
            Tipo = tipoDaConta;
            DataCadastro = dataCadastro;
            DataUltimoAcesso = dataUltimoAcesso;
            DataAlteracao = dataAlteracao;
            Saldo = saldo;
            Status = statusDaConta;
        }
    }  
}