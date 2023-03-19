using Instruction.API.Application.Commands;
using Instruction.API.Application.DTOs;
using Instruction.API.Application.Enums;
using Instruction.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Instruction.API.Controllers
{
    [ApiController]
    [Route("api/v1/users/{userId}/[controller]")]
    [Consumes("application/json")]
    public class InstructionsController : ControllerBase
    {
        private readonly ILogger<InstructionsController> _logger;
        private readonly IMediator _mediator;
        private readonly IInstructionQueries _instructionQueries;

        public InstructionsController(ILogger<InstructionsController> logger, IMediator mediator, IInstructionQueries instructionQueries)
        {
            _logger = logger;
            _mediator = mediator;
            _instructionQueries = instructionQueries;
        }

        [HttpPost]
        [ProducesResponseType(typeof(InstructionDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(int userId, [FromBody] CreateInstructionDTO request)
        {
            if (userId == default) { return BadRequest(); }

            var command = new CreateInstructionCommand(request.TransactionDay, userId, request.Amount, request.Notifications);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new {userId = result.UserId}, result);
        }

        [Route("active/cancel")]
        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Cancel(int userId)
        {
            if (userId == default) { return BadRequest(); }

            var command = new CancelInstructionCommand(userId);

            var result = await _mediator.Send(command);

            if(!result) return NotFound();

            return Ok();
        }

        [Route("active")]
        [HttpGet]
        [ProducesResponseType(typeof(InstructionDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int userId)
        {
            if (userId == 0) { return BadRequest(); }

            var result = await _instructionQueries.GetActiveAsync(userId);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [Route("active/notifications")]
        [HttpGet]
        [ProducesResponseType(typeof(List<InstructionNotification>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetNotifications(int userId)
        {
            if (userId == 0) { return BadRequest(); }

            var result = await _instructionQueries.GetNotificationsAsync(userId);

            if (!result.Any()) return NotFound();

            return Ok(result);
        }
    }
}