using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.Application.Interfaces;

namespace TicketSelling.Web.Controllers;

public class UserSavedCardsController(IMapper _mapper, IUserSavedCardService _service) : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}