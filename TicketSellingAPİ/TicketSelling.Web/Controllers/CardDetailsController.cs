using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSelling.Application.Dtos.CardDetails;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Web.Requests;

namespace TicketSelling.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardDetailsController(IMapper _mapper, ICardDetailsService  _service, UserManager<AppUser> _userManager) : Controller
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

        var createdCard = await _service.AddAsync(request); 
        if (createdCard == null)
            return BadRequest("Failed to create card details.");

        var user = await _userManager.Users
            .Include(u => u.UserSavedCards)
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user == null)
            return NotFound("User not found.");

        var savedCard = new UserSavedCard
        {
            UserId = user.Id,
            CardDetailsId = createdCard.Id
        };

        user.UserSavedCards.Add(savedCard);

        await _userManager.UpdateAsync(user);

        return CreatedAtAction(nameof(GetById), new { id = createdCard.Id }, createdCard);
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
