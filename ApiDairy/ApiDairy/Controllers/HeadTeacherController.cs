using ApiDairy.Data;
using ApiDairy.Data.Interfaces;
using ApiDairy.Data.Repositories;
using ApiDairy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiDairy.Controllers
{
    [Route("api/headteacher")]
    //[Authorize(Roles = "headteacher, admin")]
    [ApiController]
    public class HeadTeacher : ControllerBase
    {
        #region datacontext
        IBaseRepository<User> dbUser;

        IBaseRepository<Office> dbOffice;
        IBaseRepository<Klass> dbKlass;
        IBaseRepository<Subject> dbSub;
        IBaseRepository<Timetable> dbTT;
        IBaseRepository<Mark> dbMark;

        DataContext context;

        public HeadTeacher(DataContext _context)
        {
            context = _context;

            dbUser = new UserRepository(context);

            dbOffice = new OfficeRepository(context);
            dbKlass = new KlassRepository(context);
            dbSub = new SubjectRepository(context);
            dbTT = new TimetableRepository(context);
            dbMark = new MarkRepository(context);
        }

        #endregion

        /*[Route("GetExcel")]
        [HttpGet]
        public IActionResult GetExcel()
        {
            Excel.Application
                        // Start a new workbook in Excel.
                        m_objExcel = new Excel.Application();
            m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
            m_objBook = (Excel._Workbook)(m_objBooks.Add(m_objOpt));
        }*/

        #region Info
        [Route("GetInfo")]
        [HttpGet]
        public IActionResult Info()
        {
            return Content("Создание пользователя(api/headteacher/createUser). Редактирование пользователя(api/headteacher/editUser). Удаление пользователя(api/headteacher/deleteUser).");
        }
        #endregion

        #region CED on User

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

            dbUser.Create(new User    { login = user.login, 
                                        password = user.password, 
                                        roleid = user.roleid, 
                                        first_name = user.first_name, 
                                        middle_name = user.middle_name,
                                        last_name = user.last_name });
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

        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            dbUser.Delete(id);
            await context.SaveChangesAsync();
            return Ok();
        }

        #endregion

        #region CED Office

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
                dbOffice.Delete(id);
                await context.SaveChangesAsync();
                return Ok();
        }
        #endregion

        #region CED class
        [Route("createClass")]
        [HttpPost]
        public async Task<ActionResult<Klass>> CreateClass(Klass @class)
        {
            if (@class == null)
                return BadRequest();

            if (context.Klasses.Any(x => x.number == @class.number && x.letter == @class.letter))
                return BadRequest(new { errorText = "Такой класс уже существует" });

            dbKlass.Create(new Klass { number = @class.number, letter = @class.letter });
            await context.SaveChangesAsync();
            return Ok(@class);
        }

        [Route("editClass")]
        [HttpPut]
        public async Task<ActionResult<Klass>> EditClass(Klass @class)
        {
            if (@class == null)
            {
                return BadRequest();
            }
            if (!context.Klasses.Any(x => x.classid == @class.classid))
            {
                return NotFound();
            }

            dbKlass.Update(@class);
            await context.SaveChangesAsync();

            return Ok(@class);
        }

        [Route("deleteClass/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteClass(int id)
        {
                dbKlass.Delete(id);
                await context.SaveChangesAsync();
                return Ok();
        }
        #endregion

        #region CED Subject
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
                dbSub.Delete(id);
                await context.SaveChangesAsync();
                return Ok(id);
        }
        #endregion

        #region CED Timeatable
        [Route("createTimetable")]
        [HttpPost]
        public ActionResult CreateTimetable(Timetable timetable)
        {
            if (timetable == null)
                return BadRequest();

            if (context.Timetables.Any(x => x.timetableid == timetable.timetableid))
                return BadRequest(new { errorText = "Такое расписание уже сушествует" });

            dbTT.Create(new Timetable { Class = timetable.Class, Office = timetable.Office, Date = timetable.Date, Lesson = timetable.Lesson, Subject = timetable.Subject, User = timetable.User });
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
            if (!context.Timetables.Any(x => x.timetableid == timetable.timetableid))
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
                dbTT.Delete(id);
                await context.SaveChangesAsync();
                return Ok();
        }
        #endregion

        #region Reports
        #region SubReports
        private const string TABLE_NAME = "subject";

        [Route("getUserReport")]
        [HttpGet]
        public async Task<IActionResult> GetUserReport(int id)
        {
            using (DataContext db = new DataContext())
            {
                var marks = from Mark in db.Marks
                            join user in db.users on Mark.userid equals user.userid
                            select new
                            {
                                mark = Mark.mark,
                                date = Mark.date,
                                subjectid = Mark.subjectid,
                                firts_name = user.first_name,
                                middle_name = user.middle_name,
                                last_name = user.last_name
                            };

                var klass = from Klass in db.Klasses
                                 join user in db.users on Klass.userid equals user.userid
                                 select new
                                 {
                                     number = Klass.number,
                                     letter = Klass.letter,
                                 };
                return null;
;
            }

            /*string commandText = $"SELECT * FROM class, mark INNER JOIN subjectid = @id";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("id", id);

                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        Mark mark = ReadMark(reader);
                        Klass klass = ReadKlass(reader);
                        return mark, klass;
                    }
            }
            return null;*/
        }

        /*private static Mark ReadMark(NpgsqlDataReader reader)
        {
            int? markid = reader["markid"] as int?;
            DateTime? date = reader["date"] as DateTime?;
            int? subjectid = reader["subjectid"] as int?;
            int? Mark = reader["mark"] as int?;
            List<User> user = reader["user"] as List<User>;

            Mark mark = new Mark
            {
                markid = markid.Value,
                date = date.Value,
                subjectid = subjectid.Value,
                mark = Mark.Value,
                users = user
            };
            return mark;
        }

        private static Klass ReadKlass(NpgsqlDataReader reader)
        {
            int? classid = reader["classid"] as int?;
            int? number = reader["number"] as int?;
            string letter = reader["lertter"] as string;

            Klass klass = new Klass
            {
                classid = classid.Value,
                number = number.Value,
                letter = letter,
            };
            return klass;
        }*/
        #endregion
        #endregion
    }
}