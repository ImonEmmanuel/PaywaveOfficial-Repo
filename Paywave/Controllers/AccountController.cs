using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaywaveAPICore.Processor;
using PaywaveAPIData.DataService.Interface;
using PaywaveAPIData.DTO;
using PaywaveAPIData.Model;
using PaywaveAPIData.Response;
using System.ComponentModel.DataAnnotations;

namespace Paywave.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly AccountProcessor _accountProcessor;
        public AccountController(AccountProcessor accountProcessor)
        {
            _accountProcessor = accountProcessor;
        }

        [HttpPost("createAccount/{clientId}")]
        [ProducesResponseType(typeof(ServiceResponse<Account>), 200)]
        public IActionResult CreateAccount(string clientId, [Required] int transactionPin)
        {
            var res = _accountProcessor.CreateAccount(clientId, transactionPin.ToString());
            return HandleActionResult(res);
        }

        [HttpGet("getAccountDetailsByClientId/{clientId}")]
        [ProducesResponseType(typeof(ServiceResponse<Account>), 200)]
        public IActionResult GetAccountDetailsByClientId([Required]string clientId)
        {
            var res = _accountProcessor.GetAccountdetailsByClientId(clientId);
            return HandleActionResult(res);
        }

        [HttpGet("getCardDetails/{accountNumber}")]
        [ProducesResponseType(typeof(ServiceResponse<Card>), 200)]
        public IActionResult GetCardDetails([Required]string accountNumber)
        {
            var res = _accountProcessor.GetCardDetails(accountNumber);
            return HandleActionResult(res);
        }

        [HttpPost("getAccountDetails/{accountNumber}")]
        [ProducesResponseType(typeof(ServiceResponse<Account>), 200)]
        public IActionResult GetAccountDetails(string accountNumber)
        {
            var res = _accountProcessor.GetAccountdetails(accountNumber);
            return HandleActionResult(res);
        }

        [HttpPost("InitiateCardTransaction/{merchantAccount}")]
        [ProducesResponseType(typeof(ServiceResponse<Account>), 200)]
        public IActionResult InitiateCardTransaction([Required][FromBody] InitiateCardRequestDTO request, string merchantAccount)
        {
            var res = _accountProcessor.IntiateCardTransaction(request, merchantAccount);
            return HandleActionResult(res);
        }

        [HttpGet("GetAllTransaction/{accountNumber}")]
        [ProducesResponseType(typeof(ServiceResponse<Account>), 200)]
        public IActionResult GetAllTransaction(string accountNumber)
        {
            var res = _accountProcessor.GetAllTransaction(accountNumber);
            return HandleActionResult(res);
        }
    }
}
