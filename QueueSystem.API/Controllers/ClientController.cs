using Microsoft.AspNetCore.Mvc;
using QueueSystem.Domain.Dtos;
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

        [HttpPost("/queue")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientRequest request)
        {
            var client = await _clientService.RegisterClientAsync(
                request.Name, request.Category, request.Priority, request.QueueId
                );

            var position = await _clientService.ConsultPositionAsync(client.Id);

            return Ok(new { client.Id, Position = position });
        }

        [HttpGet("api/queue/{id}/position")]
        public async Task<IActionResult> ConsultPosition(int id)
        {
            var position = await _clientService.ConsultPositionAsync(id);

            return Ok(new { Position = position });
        }

        [HttpPut("api/queue/{id}")]
        public async Task<IActionResult> Unsubscribe(int id)
        {
            await _clientService.UnsubscribeAsync(id);

            return Ok();
        }
    }
}