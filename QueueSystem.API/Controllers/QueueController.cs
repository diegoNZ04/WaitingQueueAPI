using Microsoft.AspNetCore.Mvc;
using QueueSystem.Infra.Services.Interfaces;

namespace QueueSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _queueService;
        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }
        [HttpGet("api/queue")]
        public async Task<IActionResult> ListClientsInQueue([FromQuery] int queueId)
        {
            var clients = await _queueService.ListAllClientsInQueueAsync(queueId);

            if (clients == null || !clients.Any())
            {
                return NotFound(new { Message = "Não há clientes na fila" });
            }

            return Ok(clients);
        }

        [HttpPost("api/queue/next")]
        public async Task<IActionResult> CallNextClient([FromQuery] int queueId)
        {
            var client = await _queueService.CallNextClientAsync(queueId);

            if (client == null)
            {
                return NotFound(new { Message = "Não há clientes na fila" });
            }

            return Ok(client);
        }
    }
}