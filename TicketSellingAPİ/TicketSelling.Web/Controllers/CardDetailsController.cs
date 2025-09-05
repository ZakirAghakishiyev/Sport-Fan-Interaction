using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.Application.Dtos.CardDetails;
using TicketSelling.Application.Interfaces;
using TicketSelling.Web.Requests;

namespace TicketSelling.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardDetailsController(IMapper _mapper, ICardDetailsService  _service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cards = await _service.GetAllAsync();
        return Ok(cards);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var card = await _service.GetByIdAsync(id);
        if (card == null)
            return NotFound();

        return Ok(card);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CardDetailsCreateDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.AddAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var card= await _service.GetByIdAsync(id);
        if (card == null)
            return NotFound();
        await _service.RemoveAsync(id);

        return NoContent();
    }
}
