using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSelling.Application.Dtos.Match;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;

namespace TicketSelling.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MatchesController(IMatchService _service) : ControllerBase
{
    // GET: api/matches
    [HttpGet]
    public async Task<ActionResult<List<MatchDto>>> GetAll()
    {
        var matches = await _service.GetAllAsync(
            include: q => q
                .Include(m => m.Stadium)
                .Include(m => m.SectorPrices)
                    .ThenInclude(sp => sp.Sector),
            asNoTracking: true
        );

        return Ok(matches);
    }

    // GET: api/matches/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MatchDto>> Get(int id)
    {
        try
        {
            var match = await _service.GetAsync(
                m => m.Id == id,
                include: q => q
                    .Include(m => m.Stadium)
                    .Include(m => m.SectorPrices)
                        .ThenInclude(sp => sp.Sector),
                asNoTracking: true
            );

            return Ok(match);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // POST: api/matches
    [HttpPost]
    public async Task<ActionResult<MatchDto>> Create([FromBody] MatchCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    // PUT: api/matches/5
    [HttpPut("{id}")]
    public async Task<ActionResult<MatchDto>> Update(int id, [FromBody] MatchUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, dto);
        if (updated == null)
            return NotFound(new { message = "Match not found" });

        return Ok(updated);
    }

    // DELETE: api/matches/5
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
