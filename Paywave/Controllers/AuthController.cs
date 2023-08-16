using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaywaveAPICore.Authentication;
using PaywaveAPICore.Processor;
using PaywaveAPIData.Model;
using PaywaveAPIData.Response;
using PaywaveAPIData.Response.Auth;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Paywave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly AuthProcessor _authProcessor;
        public AuthController(AuthProcessor authProcessor)
        {
            _authProcessor = authProcessor;
        }
        
        [HttpPost("signUp")]
        [ProducesResponseType(typeof(ServiceResponse<SignupResp>), 200)]
        public async Task<IActionResult> SignUp([FromBody][Required] SignupModel signupModel)
        {
            var message = await _authProcessor.SignUp(signupModel);
            return HandleActionResult(message);
        }

        [HttpPost("signin")]
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        public async Task<IActionResult> Signin([FromBody][Required] LoginModel loginModel)
        {
            var message = await _authProcessor.Login(loginModel);
            return HandleActionResult(message);
        }

        [Authorize]
        [HttpPut("updateInformation")]
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        public IActionResult UpdateDetails([FromBody][Required] UpdateDataModel updateDataModel)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email).ToString();
            if (userEmail is null)
            {
                return Unauthorized("Invalid Token");
            }
            var message = _authProcessor.UpdateDetails(updateDataModel, userEmail);
            return HandleActionResult(message);
        }

        [Authorize]
        [HttpGet("getClient")]
        [ProducesResponseType(typeof(ServiceResponse<Client>), 200)]
        public IActionResult UpdateDetails()
        {
            var clientId = User.FindFirstValue("ID").ToString();
            if(clientId is null)
            {
                return Unauthorized("Invalid Token");
            }
            var message = _authProcessor.GetClient(clientId);
            return HandleActionResult(message);
        }

        
        [HttpGet("generateToken/{refreshToken}")]
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        public async Task<IActionResult> GenerateToken(string refreshToken)
        {

            //Check if the Token exist  else return Unauthroize acces please sign in
            //Check if the token has not expired  else Expired Token
            //Regenerate the Access Token by get client details and generate Token

            var message = await _authProcessor.GenerateToken(refreshToken);
            return HandleActionResult(message);

        }
    }
}
