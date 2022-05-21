using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDairy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDairy.Controllers
{
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : Controller
    {
        DataContext context;
        public AdminController(DataContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }
    }
}
