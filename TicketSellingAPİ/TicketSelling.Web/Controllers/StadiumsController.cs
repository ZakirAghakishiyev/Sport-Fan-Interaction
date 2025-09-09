using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.Application.Dtos.Stadium;
using TicketSelling.Application.Interfaces;

namespace TicketSelling.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StadiumsController(IMapper _mapper, IStadiumService _service) : Controller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StadiumDto>>> GetAll()
    {
        var stadiums = await _service.GetAllAsync();
        return Ok(stadiums);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StadiumDto>> GetById(int id)
    {
        var stadium = await _service.GetByIdAsync(id);
        if (stadium == null) return NotFound();

        return Ok(stadium);
    }

    [HttpPost]
    public async Task<ActionResult<StadiumDto>> Create([FromBody] StadiumCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<StadiumDto>> Update(int id, [FromBody] StadiumUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _service.UpdateAsync(id,dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.RemoveAsync(id);
        return NoContent();
    }
}
