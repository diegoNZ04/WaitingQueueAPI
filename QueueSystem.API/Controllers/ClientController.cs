using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueueSystem.Application.Dtos;
using QueueSystem.Infra.Services.Interfaces;

namespace QueueSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost("api/client")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientRequest request)
        {
            var client = await _clientService.RegisterClientAsync(
                request.Name, request.Category, request.Priority, request.QueueId
                );

            var position = await _clientService.ConsultPositionAsync(client.Id);

            return Ok(new { client.Id, Position = position });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("api/client/{id}/position")]
        public async Task<IActionResult> ConsultPosition(int id)
        {
            var position = await _clientService.ConsultPositionAsync(id);

            return Ok(new { Position = position });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpDelete("api/client/{id}")]
        public async Task<IActionResult> Unsubscribe(int id)
        {
            await _clientService.UnsubscribeAsync(id);

            return Ok();
        }
    }
}