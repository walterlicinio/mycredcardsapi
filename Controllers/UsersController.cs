using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myCredCardsAPI.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCredCardsAPI.Controllers
{
    [Route("api/[controller]")] // Rota padrão para acessar API
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersService _usersService;
        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("create-card")]
        public IActionResult CreateUser(String mail)
        {
            var response = _usersService.AddUser(mail);
             return Ok(response);
        }


        [HttpGet("check-cards/{mail}")]
        public IActionResult GetCards(String mail)
        {
            var response = _usersService.GetUserCards(mail);
            if (response != null)
            { return Ok(response); }
            return NotFound();
        }

    }
}
