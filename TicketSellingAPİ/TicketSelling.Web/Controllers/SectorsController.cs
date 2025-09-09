using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSelling.Application.Dtos.Sector;
using TicketSelling.Application.Interfaces;

namespace TicketSelling.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SectorsController(IMapper _mapper, ISectorService _service) : Controller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SectorDto>>> GetAll()
    {
        var sectors = await _service.GetAllAsync(
            include: query => query.Include(s => s.Seats).Include(s => s.Stadium)
        );
        return Ok(sectors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SectorDto>> GetById(int id)
    {
        var sector = await _service.GetByIdAsync(id);
        if (sector == null) return NotFound();

        return Ok(sector);
    }

    [HttpPost]
    public async Task<ActionResult<SectorDto>> Create([FromBody] SectorCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SectorDto>> Update(int id, [FromBody] SectorUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, dto);
        if (updated == null) return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.RemoveAsync(id);
        return NoContent();
    }
}
