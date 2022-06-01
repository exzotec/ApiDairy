using Microsoft.AspNetCore.Mvc;
using ApiDairy.Models;
using System.Collections.Generic;
using ApiDairy.Data.Interfaces;
using ApiDairy.Data.Repositories;
using ApiDairy.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace ApiDairy.Controllers
{
    [Route("api/student")]
    //[Authorize(Roles = "student, parent")]
    [ApiController]
    public class StudentAndParent : ControllerBase
    {
        IBaseRepository<Mark> dbMark;
        IBaseRepository<Hometask> dbHometask;
        IBaseRepository<Timetable> dbTT;

        DataContext context;

        public StudentAndParent(DataContext _context)
        {
            context = _context;

            dbMark = new MarkRepository(context);
            dbHometask = new HometaskRepository(context);
            dbTT = new TimetableRepository(context);
        }

        [Route("GetAllMarks")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mark>>> GetAllMarks()
        {
            return await context.Marks.ToListAsync();
        }

        [Route("GetAllTimetables")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timetable>>> GetAllTimetables()
        {
            return await context.Timetables.ToListAsync();
        }

        [Route("GetAllHometasks")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hometask>>> GetAllHometasks()
        {
            return await context.Hometasks.ToListAsync();
        }
    }
}
