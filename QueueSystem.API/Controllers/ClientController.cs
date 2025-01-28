using Microsoft.AspNetCore.Mvc;
using QueueSystem.Domain.Dtos;
using QueueSystem.Infra.Interfaces;

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

        [HttpGet("{queueId}/clients/{clientId}/position")]
        public async Task<IActionResult> GetClientPosition(int queueId, int clientId)
        {
            try
            {
                var position = await _clientService.GetClientPositionAsync(queueId, clientId);
                return Ok(new { Position = position });
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        [HttpPost("{queueId}/clients")]
        public async Task<IActionResult> RegisterClient(int queueId, [FromBody] ClientDto clientDto)
        {
            try
            {
                var client = await _clientService.AddClientToQueueAsync(queueId, clientDto);
                return CreatedAtAction(nameof(GetClientPosition), new { queueId, clientId = client.Id }, client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{queueId}/clients/{clientId}")]
        public async Task<IActionResult> CancelClient(int queueId, int clientId)
        {
            try
            {
                await _clientService.RemoveClientFromQueueAsync(queueId, clientId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
}