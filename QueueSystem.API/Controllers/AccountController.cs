using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueueSystem.Domain.Dtos;
using QueueSystem.Infra.Services.Interfaces;

namespace QueueSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("api/auth/register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var result = await _accountService.RegisterUserAsync(request);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(new { message = "Usuário registrado com sucesso!" });
        }
    }
}