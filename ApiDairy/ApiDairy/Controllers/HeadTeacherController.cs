using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDairy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDairy.Controllers
{
    [Route("api/headteacher")]
    [Authorize(Roles = "headteacher, admin")]
    [ApiController]
    public class HeadTeacherController : ControllerBase
    {
        private readonly DataContext db;

        public HeadTeacherController(DataContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Info()
        {
            return Content("Создание пользователя(api/headteacher/createUser). Редактирование пользователя(api/headteacher/editUser). Удаление пользователя(api/headteacher/deleteUser).");
        }

        //CED on User
        #region

        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await db.Users.ToListAsync();
        }

        [Route("createUser")]
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            if (user == null)
                return BadRequest();

            if (db.Users.Any(u => u.Login == user.Login))
                return BadRequest(new { errorText = "Пользователь с таким логином уже существует" });

            db.Users.Add(new User { UserId = Guid.NewGuid().ToString(), Role = user.Role, Login = user.Login, Password = "" });
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [Route("editUser")]
        [HttpPut]
        public async Task<ActionResult<User>> PutUser(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(u => u.UserId == user.UserId))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        [Route("deleteUser")]
        [HttpPost]
        public async Task<IActionResult> Delete(User user)
        {
            if (user != null)
            {
                User u = await db.Users.FirstOrDefaultAsync(u => u.UserId == u.UserId);
                if (u != null)
                {
                    db.Users.Remove(u);
                    await db.SaveChangesAsync();
                    return Ok(u);
                }
            }
            return NotFound();
        }

        #endregion

        //CED Office
        #region
        [Route("createOffice")]
        [HttpPost]
        public async Task<ActionResult<Office>> AddOffice(Office office)
        {
            if (office == null)
                return BadRequest();

            if (db.Offices.Any(o => o.Number == office.Number))
                return BadRequest(new { errorText = "Кабинет с таким номером уже существует" });

            db.Offices.Add(new Office { OfficeId = Guid.NewGuid().ToString(), Number = office.Number });
            await db.SaveChangesAsync();
            return Ok(office);
        }

        [Route("editOffice")]
        [HttpPut]
        public async Task<ActionResult<Office>> PutOffice(Office office)
        {
            if (office == null)
            {
                return BadRequest();
            }
            if (!db.Offices.Any(o => o.OfficeId == office.OfficeId))
            {
                return NotFound();
            }

            db.Update(office);
            await db.SaveChangesAsync();

            return Ok(office);
        }

        [Route("deleteOffice")]
        [HttpPost]
        public async Task<IActionResult> Delete(Office office)
        {
            if (office != null)
            {
                Office o = await db.Offices.FirstOrDefaultAsync(o => o.OfficeId == o.OfficeId);
                if (o != null)
                {
                    db.Offices.Remove(o);
                    await db.SaveChangesAsync();
                    return Ok(o);
                }
            }
            return NotFound();
        }
        #endregion

        //CED class
        #region
        [Route("createClass")]
        [HttpPost]
        public async Task<ActionResult<Class>> AddClass(Class @class)
        {
            if (@class == null)
                return BadRequest();

            if (db.Classes.Any(c => c.Number == @class.Number && c.Letter == @class.Letter))
                return BadRequest(new { errorText = "Такой класс уже существует" });

            db.Classes.Add(new Class { ClassId = Guid.NewGuid().ToString(), Number = @class.Number, Letter = @class.Letter });
            await db.SaveChangesAsync();
            return Ok(@class);
        }

        [Route("editClass")]
        [HttpPut]
        public async Task<ActionResult<Class>> PutClass(Class @class)
        {
            if (@class == null)
            {
                return BadRequest();
            }
            if (!db.Classes.Any(c => c.ClassId == @class.ClassId))
            {
                return NotFound();
            }

            db.Update(@class);
            await db.SaveChangesAsync();

            return Ok(@class);
        }

        [Route("deleteClass")]
        [HttpPost]
        public async Task<IActionResult> DeleteClass(Class @class)
        {
            if (@class != null)
            {
                Class c = await db.Classes.FirstOrDefaultAsync(c => c.ClassId == c.ClassId);
                if (c != null)
                {
                    db.Classes.Remove(c);
                    await db.SaveChangesAsync();
                    return Ok(c);
                }
            }
            return NotFound();
        }

        #endregion

        //CED Subject
        #region
        [Route("createSubject")]
        [HttpPost]
        public async Task<ActionResult<Subject>> AddSubject(Subject subject)
        {
            if (subject == null)
                return BadRequest();

            if (db.Subjects.Any(s => s.Name == subject.Name))
                return BadRequest(new { errorText = "Такой класс уже существует" });

            db.Subjects.Add(new Subject { SubjectId = Guid.NewGuid().ToString(), Name = subject.Name });
            await db.SaveChangesAsync();
            return Ok(subject);
        }

        [Route("editSubject")]
        [HttpPut]
        public async Task<ActionResult<Subject>> PutSubject(Subject subject)
        {
            if (subject == null)
            {
                return BadRequest();
            }
            if (!db.Subjects.Any(s => s.SubjectId == subject.SubjectId))
            {
                return NotFound();
            }

            db.Update(subject);
            await db.SaveChangesAsync();

            return Ok(subject);
        }

        [Route("deleteSubject")]
        [HttpPost]
        public async Task<IActionResult> Delete(Subject sub)
        {
            if (sub != null)
            {
                Subject subject = await db.Subjects.FirstOrDefaultAsync(s => s.SubjectId == sub.SubjectId);
                if (subject != null)
                {
                    db.Subjects.Remove(subject);
                    await db.SaveChangesAsync();
                    return Ok(subject);
                }
            }
            return NotFound();
        }
        #endregion
    }
}