using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSelling.Application.Dtos.Payment;
using TicketSelling.Application.Interfaces;

namespace TicketSelling.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController(IMapper _mapper, IPaymentService _service) : Controller
{
    // GET: api/payments
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var payments = await _service.GetAllAsync(
            include: q => q
                .Include(p => p.Order)
                .Include(p => p.PaymentDetail)
        );
        return Ok(payments);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var payment = await _service.GetByIdAsync(id);
        if (payment == null)
            return NotFound();

        return Ok(payment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PaymentCreateDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.AddAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PaymentUpdateDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.UpdateAsync(updateDto);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
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