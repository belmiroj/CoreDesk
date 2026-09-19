using CoreDesk.API.DTOs;
using CoreDesk.API.Enums;
using CoreDesk.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDesk.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/chamados")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketsController(ITicketService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Open([FromBody] CreateTicketRequest request)
        {
            var created = await _service.OpenTicketAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await _service.GetByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetFiltered(
            [FromQuery] TicketStatus? status, 
            [FromQuery] TicketPriority? priority, 
            [FromQuery] int? categoryId)
        {
            return Ok(await _service.GetFilteredAsync(status, priority, categoryId));
        }

        [HttpPatch("{id:int}/iniciar")]
        public async Task<IActionResult> StartService(int id)
        {
            await _service.StartServiceAsync(id);
            return NoContent();
        }

        [HttpPatch("{id:int}/encerrar")]
        public async Task<IActionResult> Close(int id, [FromBody] CloseTicketRequest request)
        {
            await _service.CloseTicketAsync(id, request);
            return NoContent();
        }

        [HttpPost("{id:int}/interacoes")]
        public async Task<IActionResult> AddInteraction(int id, [FromBody] AddInteractionRequest request)
        {
            await _service.AddInteractionAsync(id, request);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}