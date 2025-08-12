using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.Application.Dtos.Merchandise;
using TicketSelling.Application.Interfaces;

namespace TicketSelling.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MerchandiseController(IMerchandiseService _service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var merch = _service.GetAll();
        return Ok(merch);
    }

    [HttpPost]
    public IActionResult Create([FromBody]MerchandiseCreateDto createDto)
    {
        if (createDto == null) throw new NullReferenceException();
        _service.AddAsync(createDto);
        return Ok(createDto);
    }

}
