using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueueSystem.Application.Dtos;
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

        [Authorize(Roles = "Admin")]
        [HttpPost("/api/queue/create")]
        public async Task<IActionResult> CreateQueue([FromBody] CreateQueueRequest request)
        {
            var queue = await _queueService.CreateQueueAsync(request.Category, request.Description);
            return Ok(queue);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("api/queue/list")]
        public async Task<IActionResult> ListClientsInQueue([FromQuery] int queueId)
        {
            var clients = await _queueService.ListAllClientsInQueueAsync(queueId);

            if (clients == null || clients.Count == 0)
            {
                return NotFound(new { Message = "Não há clientes na fila" });
            }

            return Ok(clients);
        }

        [Authorize(Roles = "Admin")]
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