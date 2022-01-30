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

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> BuscarContaPorNumero(int numeroConta)
        {
            var account = await _accountService.BuscarContaPorNumeroAsync(numeroConta);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarNovaConta(string id, int numeroConta, Client cliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {
            dataCadastro = DateTime.Now;
            dataAlteracao = DateTime.Now;
            saldo = 0;

            await _accountService.CadastrarContaAsync(id, numeroConta, cliente, tipoDaConta, dataCadastro, dataUltimoAcesso, dataAlteracao, saldo, statusDaConta);
            return Ok();
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
            {
                return NotFound();
            }
            await _accountService.ExcluirContaAsync(numeroConta);
            return NoContent();
        }
    }
}
