using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiDairy.Data;

namespace ApiDairy.Controllers
{
    [Route("api/teach")]
    [Authorize(Roles = "admin, headteacher, teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        DataContext context;
        public TeacherController(DataContext _context)
        {
            context = _context;
        }
    }
}
