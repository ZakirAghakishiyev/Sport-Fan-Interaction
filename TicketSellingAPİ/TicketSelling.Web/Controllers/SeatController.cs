using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.Application.Dtos.Seat;
using TicketSelling.Application.Interfaces;

namespace TicketSelling.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatController(IMapper _mapper, ISeatService _service) : Controller
{
    [HttpGet("{id}")]
    public async Task<ActionResult<SeatDto>> GetById(int id)
    {
        var seat = await _service.GetByIdAsync(id);
        if (seat == null) return NotFound();
        return Ok(seat);
    }

    [HttpGet]
    public async Task<ActionResult<List<SeatDto>>> GetAll()
    {
        var seats = await _service.GetAllAsync();
        return Ok(seats);
    }

    [HttpPost]
    public async Task<ActionResult<SeatDto>> Create([FromBody] SeatCreateDto createDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var seat = await _service.AddAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = seat.Id }, seat);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SeatDto>> Update(int id, [FromBody] SeatUpdateDto updateDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existingSeat = await _service.GetByIdAsync(id);
        if (existingSeat == null) return NotFound();

        // preserve ID
        var updatedSeat = await _service.UpdateAsync(id,updateDto);
        return Ok(updatedSeat);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingSeat = await _service.GetByIdAsync(id);
        if (existingSeat == null) return NotFound();

        await _service.RemoveAsync(id);
        return NoContent();
    }
}