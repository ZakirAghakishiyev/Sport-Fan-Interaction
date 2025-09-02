using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.Application.Dtos.Merchandise;
using TicketSelling.Application.Interfaces;
using TicketSelling.Core.Entities;
using TicketSelling.Web.Requests;

namespace TicketSelling.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MerchandiseController(IMerchandiseService _service, IMapper _mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var merch =await _service.GetAllAsync();
        return Ok(merch);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]MerchandiseCreateDto createDto)
    {
        if (createDto == null) throw new NullReferenceException();
        var newMerchandise= await _service.AddAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = newMerchandise.Id }, newMerchandise);
    }

    [HttpGet("/{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var merch=await _service.GetByIdAsync(id);
        if (merch == null) throw new NullReferenceException();
        return Ok(merch);
    }

    [HttpPut("/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MerchandiseUpdateRequest req)
    {
        var merch=GetById(id);
        if(merch == null) throw new NullReferenceException();
        var updateDto = _mapper.Map<MerchandiseUpdateDto>(req);
        updateDto.Id = id;
        var res=await _service.UpdateAsync(updateDto);
        return Ok(res);
    }

    [HttpDelete("/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var merch = GetById(id);
        if (merch == null) throw new NullReferenceException();
        await _service.RemoveAsync(id);
        return Ok(merch.Id);
    }


}

