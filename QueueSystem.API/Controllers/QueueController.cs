using Microsoft.AspNetCore.Mvc;
using QueueSystem.Domain.Dtos;
using QueueSystem.Infra.Interfaces;

namespace QueueSystem.API.Controllers
{
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpGet("{queueId}/clients/{clientId}/position")]
        public async Task<IActionResult> GetClientPosition(int queueId, int clientId)
        {
            try
            {
                var position = await _queueService.GetClientPositionAsync(queueId, clientId);
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
                var client = await _queueService.AddClientToQueueAsync(queueId, clientDto);
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
                await _queueService.RemoveClientFromQueueAsync(queueId, clientId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
}