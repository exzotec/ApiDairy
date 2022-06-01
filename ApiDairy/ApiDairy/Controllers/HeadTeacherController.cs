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
    [Route("api/headteacher")]
    //[Authorize(Roles = "headteacher, admin")]
    [ApiController]
    public class HeadTeacher : ControllerBase
    {
        IBaseRepository<User> dbUser;

        IBaseRepository<Office> dbOffice;
        IBaseRepository<Class> dbClass;
        IBaseRepository<Subject> dbSub;
        IBaseRepository<Timetable> dbTT;

        DataContext context;

        public HeadTeacher(DataContext _context)
        {
            context = _context;

            dbUser = new UserRepository(context);

            dbOffice = new OfficeRepository(context);
            dbClass = new ClassRepository(context);
            dbSub = new SubjectRepository(context);
            dbTT = new TimetableRepository(context);
        }

        //Get Info
        #region Info
        [Route("GetInfo")]
        [HttpGet]
        public IActionResult Info()
        {
            return Content("Создание пользователя(api/headteacher/createUser). Редактирование пользователя(api/headteacher/editUser). Удаление пользователя(api/headteacher/deleteUser).");
        }
        #endregion

        //CED on User
        #region

        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await context.users.ToListAsync();
        }

        [Route("createUser")]
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            if (user == null)
                return BadRequest();

            if (context.users.Any(x => x.login == user.login))
                return BadRequest(new { errorText = "Пользователь с таким логином уже существует" });

            dbUser.Create(new User { login = user.login, password = user.password, roleid = user.roleid });
            dbUser.Save();
            return Ok(user);
        }

        [Route("editUser")]
        [HttpPut]
        public async Task<ActionResult<User>> EditUser(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!context.users.Any(x => x.userid == user.userid))
            {
                return NotFound();
            }

            dbUser.Update(user);
            await context.SaveChangesAsync();

            return Ok(user);
        }

        [Route("deleteUser")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id != null)
            {
                dbUser.Delete(id);
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        #endregion

        //CED Office
        #region
        [Route("createOffice")]
        [HttpPost]
        public async Task<ActionResult<Office>> CreateOffice(Office office)
        {
            if (office == null)
                return BadRequest();

            if (context.Offices.Any(x => x.Number == office.Number))
                return BadRequest(new { errorText = "Кабинет с таким номером уже существует" });

            dbOffice.Create(new Office { Number = office.Number });
            await context.SaveChangesAsync();
            return Ok(office);
        }

        [Route("editOffice")]
        [HttpPut]
        public async Task<ActionResult<Office>> EditOffice(Office office)
        {
            if (office == null)
            {
                return BadRequest();
            }
            if (!context.Offices.Any(x => x.OfficeId == office.OfficeId))
            {
                return NotFound();
            }

            dbOffice.Update(office);
            await context.SaveChangesAsync();

            return Ok(office);
        }

        [Route("deleteOffice")]
        [HttpPost]
        public async Task<IActionResult> DeleteOffice(int id)
        {
            if (id != null)
            {
                dbOffice.Delete(id);
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
        #endregion

        //CED class
        #region
        [Route("createClass")]
        [HttpPost]
        public async Task<ActionResult<Class>> CreateClass(Class @class)
        {
            if (@class == null)
                return BadRequest();

            if (context.Classes.Any(x => x.Number == @class.Number && x.Letter == @class.Letter))
                return BadRequest(new { errorText = "Такой класс уже существует" });

            dbClass.Create(new Class { Number = @class.Number, Letter = @class.Letter });
            await context.SaveChangesAsync();
            return Ok(@class);
        }

        [Route("editClass")]
        [HttpPut]
        public async Task<ActionResult<Class>> EditClass(Class @class)
        {
            if (@class == null)
            {
                return BadRequest();
            }
            if (!context.Classes.Any(x => x.ClassId == @class.ClassId))
            {
                return NotFound();
            }

            dbClass.Update(@class);
            await context.SaveChangesAsync();

            return Ok(@class);
        }

        [Route("deleteClass")]
        [HttpPost]
        public async Task<IActionResult> DeleteClass(int id)
        {
            if (id != null)
            {
                dbClass.Delete(id);
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
        #endregion

        //CED Subject
        #region
        [Route("createSubject")]
        [HttpPost]
        public async Task<ActionResult<Subject>> CreateSub(Subject subject)
        {
            if (subject == null)
                return BadRequest();

            if (context.Subjects.Any(x => x.Name == subject.Name))
                return BadRequest(new { errorText = "Такой класс уже существует" });

            dbSub.Create(new Subject { Name = subject.Name });
            await context.SaveChangesAsync();
            return Ok(subject);
        }

        [Route("editSubject")]
        [HttpPut]
        public async Task<ActionResult<Subject>> EditSub(Subject subject)
        {
            if (subject == null)
            {
                return BadRequest();
            }
            if (!context.Subjects.Any(x => x.SubjectId == subject.SubjectId))
            {
                return NotFound();
            }

            dbSub.Update(subject);
            await context.SaveChangesAsync();

            return Ok(subject);
        }

        [Route("deleteSubject")]
        [HttpPost]
        public async Task<IActionResult> DeleteSub(int id)
        {
            if (id != null)
            {
                dbSub.Delete(id);
                await context.SaveChangesAsync();
                return Ok(id);
            }
            return NotFound();
        }
        #endregion

        //CED Timeatable
        #region
        [Route("createTimetable")]
        [HttpPost]
        public ActionResult CreateTimetable(Timetable timetable)
        {
            if (timetable == null)
                return BadRequest();

            if (context.Timetables.Any(x => x.Id == timetable.Id))
                return BadRequest(new { errorText = "Такое расписание уже сушествует" });

            dbTT.Create(new Timetable {Class = timetable.Class, Office = timetable.Office, Date = timetable.Date, Lesson = timetable.Lesson, Subject = timetable.Subject, User = timetable.User });
            dbTT.Save();
            return Ok(timetable);
        }

        [Route("editTimetable")]
        [HttpPut]
        public async Task<ActionResult<User>> EditTimetable(Timetable timetable)
        {
            if (timetable == null)
            {
                return BadRequest();
            }
            if (!context.Timetables.Any(x => x.Id == timetable.Id))
            {
                return NotFound();
            }

            dbTT.Update(timetable);
            await context.SaveChangesAsync();

            return Ok(timetable);
        }

        [Route("deleteTimetable")]
        [HttpPost]
        public async Task<IActionResult> DeleteTimetable(int id)
        {
            if (id != null)
            {
                dbTT.Delete(id);
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
        #endregion
    }

}
