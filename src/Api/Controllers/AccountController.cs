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
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> BuscarContaPorId(string id)
        {
            var account = await _accountService.BuscarContaPorIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarNovaConta(string id, int numeroConta, string idCliente, AccountType tipoDaConta, DateTime dataCadastro, DateTime dataUltimoAcesso, DateTime dataAlteracao, decimal saldo, AccountStatus statusDaConta)
        {

            dataCadastro = DateTime.Now;
            dataAlteracao = DateTime.Now;
            saldo = 0;

            await _accountService.CadastrarContaAsync(id, numeroConta, idCliente, tipoDaConta, dataCadastro, dataUltimoAcesso, dataAlteracao, saldo, statusDaConta);
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
