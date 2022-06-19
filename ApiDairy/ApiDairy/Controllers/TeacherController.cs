using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiDairy.Data;
using ApiDairy.Data.Interfaces;
using ApiDairy.Models;
using ApiDairy.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ApiDairy.Services;

namespace ApiDairy.Controllers
{
    [Route("api/teach")]
    [Authorize(Roles = "admin, headteacher, teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        HometaskService hometaskService;

        DataContext context;
        public TeacherController(DataContext _context, HometaskService service)
        {
            hometaskService = service;

            context = _context;

            dbTT = new TimetableRepository(context);
            dbHometask = new HometaskRepository(context);
            dbMark = new MarkRepository(context);
        }

        IBaseRepository<Timetable> dbTT;
        IBaseRepository<Hometask> dbHometask;
        IBaseRepository<Mark> dbMark;

        [Route("GetTimetableAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timetable>>> GetTimetableAll()
        {
            return await context.Timetables.ToListAsync();
        }

        /*[Route("GetTimetableOwn")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timetable>>> GetTimetableOwn()
        {
            if (context.users.userid == context.Timetables.userid )
            return await context.Timetables.ToListAsync();
        }*/

        [Route("CreateHometask")]
        [HttpPost]
        public async Task<IActionResult> CreateHometask(Hometask hometask)
        {

            if (hometask == null)
                return BadRequest();

            if (context.Hometasks.Any(x => x.id == hometask.id))
                return BadRequest(new { errorText = "Домашнее задание на этот день уже есть" });

            await hometaskService.AddHometask(new Hometask
            {
                id = hometask.id,
                subjectid = hometask.subjectid,
                task = hometask.task,
                date = hometask.date
            });

            if (hometask != null)
                return Content($"Hometask: {hometask.id}");
            return Content("Hometask not found");
        }

        [Route("CreateMark")]
        [HttpPost]
        public ActionResult CreateMark(Mark mark)
        {
            if (mark == null)
                return BadRequest();

            if (context.Marks.Any(x => x.markid == mark.markid))
                return BadRequest(new { errorText = "Оценка на этот день уже стоит" });

            dbMark.Create(new Mark
            {
                markid = mark.markid,
                date = mark.date,
                subjectid = mark.subjectid,
                mark = mark.mark
            });
            dbMark.Save();
            return Ok(mark);
        }
    }
}
