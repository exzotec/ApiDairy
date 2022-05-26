using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiDairy.Models;
using System.Collections.Generic;
using ApiDairy.Data.Interfaces;
using ApiDairy.Data.Repositories;

namespace ApiDairy.Controllers
{
    [Route("api/headteacher")]
    [Authorize(Roles = "headteacher, admin")]
    [ApiController]
    public class HeadTeacherController : Controller
    {
        IBaseRepository<User> dbUser;
        IBaseRepository<Office> dbOffice;
        IBaseRepository<Class> dbClass;
        IBaseRepository<Subject> dbSub;

        public HeadTeacherController()
        {
            dbUser = new UserRepository();
            dbOffice = new OfficeRepository();
            dbClass = new ClassRepository();
            dbSub = new SubjectRepository();
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
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return View(dbUser.GetList());
        }

        [Route("createUser")]
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                dbUser.Create(user);
                dbUser.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [Route("editUser")]
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                dbUser.Update(user);
                dbUser.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [Route("deleteUser")]
        [HttpPost]
        public ActionResult DeleteUserS(int id)
        {
            dbUser.Delete(id);
            return RedirectToAction("Index");
        }

        #endregion 

        //CED Office
        #region
        [Route("createOffice")]
        [HttpPost]
        public ActionResult Create(Office office)
        {
            if (ModelState.IsValid)
            {
                dbOffice.Create(office);
                dbOffice.Save();
                return RedirectToAction("Index");
            }
            return View(office);
        }

        [Route("editOffice")]
        [HttpPost]
        public ActionResult Edit(Office office)
        {
            if (ModelState.IsValid)
            {
                dbOffice.Update(office);
                dbOffice.Save();
                return RedirectToAction("Index");
            }
            return View(office);
        }

        [Route("deleteOffice")]
        [HttpPost]
        public ActionResult DeleteOffice(int id)
        {
            dbOffice.Delete(id);
            return RedirectToAction("Index");
        }
        #endregion

        //CED class
        #region
        [Route("createClass")]
        [HttpPost]
        public ActionResult Create(Class item)
        {
            if (ModelState.IsValid)
            {
                dbClass.Create(item);
                dbClass.Save();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [Route("editClass")]
        [HttpPut]
        public ActionResult Edit(Class @class)
        {
            if (ModelState.IsValid)
            {
                dbClass.Update(@class);
                dbClass.Save();
                return RedirectToAction("Index");
            }
            return View(@class);
        }

        [Route("deleteClass")]
        [HttpPost]
        public ActionResult DeleteClass(int id)
        {
            dbClass.Delete(id);
            return RedirectToAction("Index");
        }
        #endregion

        //CED Subject
        #region
        [Route("createSubject")]
        [HttpPost]
        public ActionResult Create(Subject sub)
        {
            if (ModelState.IsValid)
            {
                dbSub.Create(sub);
                dbSub.Save();
                return RedirectToAction("Index");
            }
            return View(sub);
        }

        [Route("editSubject")]
        [HttpPut]
        public ActionResult Edit(Subject sub)
        {
            if (ModelState.IsValid)
            {
                dbSub.Update(sub);
                dbSub.Save();
                return RedirectToAction("Index");
            }
            return View(sub);
        }

        [Route("deleteSubject")]
        [HttpPost]
        public ActionResult DeleteSubject(int id)
        {
            dbSub.Delete(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}