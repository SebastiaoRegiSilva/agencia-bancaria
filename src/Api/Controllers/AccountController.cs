using Agencia.Plataforma.Domain.Accounts;
using Agencia.Plataforma.Domain.Clients;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Agencia.Plataforma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly ClientService _clientService;

        public AccountController(AccountService accountService, ClientService clientService)
        {
            _accountService = accountService;
            _clientService = clientService;
        }

        [HttpGet("{numeroConta}")]
        public async Task<ActionResult<Account>> BuscarContaPorNumero(int numeroConta)
        {
            var account = await _accountService.BuscarContaPorNumeroAsync(numeroConta);
            if (account == null)
                return NotFound();
            
            return account;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> BuscarContaPorId(string id)
        {
            var account = await _accountService.BuscarContaPorIdAsync(id);
            if (account == null)
                return NotFound();
            
            return account;
        }

        [HttpPost("{idCliente}")]
        public async Task<IActionResult> CadastrarNovaConta(string idCliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {
            // Recuperar um clienta para cadastrar uma nova conta.
            var clienteRecuperado = await _clientService.BuscarClientePorIdAsync(idCliente);

            dataCadastro = DateTime.Now;
            dataAlteracao = DateTime.Now;
            
            await _accountService.CadastrarContaAsync(idCliente, tipoDaConta, dataCadastro, dataUltimoAcesso, dataAlteracao, saldo, statusDaConta);
            
            return Ok($"O cliente {clienteRecuperado.Nome} agora possui uma conta.");
        }

        [HttpPut]
        public async Task<IActionResult> EditarConta(string id, int numeroConta, Client cliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {
            await _accountService.EditarContaAsync(id, numeroConta, cliente, tipoDaConta, dataCadastro, dataUltimoAcesso, dataAlteracao, saldo, statusDaConta);
            
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int numeroConta)
        {
            var account = await _accountService.BuscarContaPorNumeroAsync(numeroConta);
            if (account == null)
                return NotFound();
            
            await _accountService.ExcluirContaAsync(numeroConta);
            return NoContent();
        }

        [HttpPost, Route("Depositar")]
        public async Task<ActionResult> Depositar(int numeroConta, double valor)
        {
            await _accountService.DepositarAsync(numeroConta, (decimal)valor);
            return Ok($"Depositado!");
        }

        [HttpGet, Route("VerificarSaldo")]
        public async Task<ActionResult> VerificarSaldo(int numeroConta)
        {
            decimal saldo = await _accountService.VerificarSaldoAsync(numeroConta);
            
            return Ok($"R${saldo} em conta!");
        }
    
    }
}
