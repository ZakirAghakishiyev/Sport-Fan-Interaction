using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSelling.Application.Dtos.Ticket;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController(IMapper _mapper, ITicketService _service) : ControllerBase
{
    // GET: api/tickets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetAll()
    {
        var tickets = await _service.GetAllAsync(
            include: q => q
                .Include(t => t.Match).ThenInclude(m => m.Stadium)
                .Include(t => t.Seat).ThenInclude(s => s.Sector)
                .Include(t => t.User));

        return Ok(tickets);
    }

    // GET: api/tickets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetById(int id)
    {
        try
        {
            var ticket = await _service.GetAsync(
                t => t.Id == id,
                include: q => q
                    .Include(t => t.Match).ThenInclude(m => m.Stadium)
                    .Include(t => t.Seat).ThenInclude(s => s.Sector)
                    .Include(t => t.User));

            return Ok(ticket);
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }

    // POST: api/tickets
    [HttpPost]
    public async Task<ActionResult<TicketDto>> Create([FromBody] TicketCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.AddAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // DELETE: api/tickets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.RemoveAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
