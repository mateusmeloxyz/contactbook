using MediatR;
using Microsoft.AspNetCore.Mvc;
using ContactBook.Application.Commands.AddContact;
using ContactBook.Application.Commands.DeleteContact;
using ContactBook.Application.Commands.UpdateContact;
using ContactBook.Application.Queries.GetAllContacts;
using ContactBook.Application.Queries.GetScheduleById;
using ContactBook.Domain.Entities;


namespace ContactBook.API.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetAll()
        {
            GetAllContactsQuery getAllContactQuery = new();
            List<Contact> projects = await _mediator.Send(getAllContactQuery);

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetContactByIdQuery(id);

            var project = await _mediator.Send(query);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddContactCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateContactCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteContactCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
