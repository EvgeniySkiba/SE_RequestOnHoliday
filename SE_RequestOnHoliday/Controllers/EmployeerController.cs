using SE_RequestOnHoliday.Enums;
using SE_RequestOnHoliday.Models;
using SE_RequestOnHoliday.Repository.Abstract;
using SE_RequestOnHoliday.Repository.Concrete;
using SE_RequestOnHoliday.Repository.DTO;
using SE_RequestOnHoliday.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace SE_RequestOnHoliday.Controllers
{
    [Authorize(Roles = "Admin, User, HR")]
    public class EmployeerController : Controller
    {
        IEmployeeRepository repo;
        IRoleRepository rolesRepo;
        IRestHolidayRepository restRepo;
        IRestTypeRepository restTypeRepo;

        public EmployeerController()
        {
            repo = new EmployeerRepository();
            restRepo = new RestHolidayRepository();
            restTypeRepo = new RestTypeRepository();
            rolesRepo = new RoleRepository();
        }

        EmployersContext db = new EmployersContext();
        // GET: Employeer
        public ActionResult List()
        {
            IEnumerable<EmployeerVM> entries = repo.getAll();
            return View(entries);
        }


        public ActionResult Index()
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string login = ticket.Name; //You have the UserName!

            EmployeerDTO empl = repo.getByLogin(login);      
            ViewBag.EmployeerID = empl.Id;

            IEnumerable<RestVM> entries = restRepo.listByEmployer(login);
            return View(entries);

        }


        // GET: Employeer/Create
        public ActionResult Create()
        {
            bindRolesList(null);
            return View();
        }

        void bindRolesList(RestTypeDTO selectedRole)
        {
            SelectList roles = new SelectList(rolesRepo.getAll(), "id", "name", selectedRole);
            ViewBag.Role = roles;
        }

        // POST: Employeer/Create
        [HttpPost]
        public ActionResult Create(SE_RequestOnHoliday.Repository.DTO.EmployeerDTO employee)
        {
            bindRolesList(null);

            if (employee.FirtstName.Length == 13)
            {
                ModelState.AddModelError("Login", "Некорректное имя(13 символов)");
            }

            if (ModelState.IsValid)
            {

                if (repo.isLoginAvailable(employee.Login))
                {
                    TempData["message"] = "Указанный логин занят";
                    ModelState.AddModelError("Login", "Некорректное имя(13 символов)");
                    return View();
                }

                try
                {
                    repo.create(employee);
                    repo.save();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Employeer/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
                return HttpNotFound();

            var empl = repo.get(id);

            SelectList roles = new SelectList(rolesRepo.getAll(), "id", "name", null);
            ViewBag.Role = roles;

            if (empl == null)
                return HttpNotFound();

            return View(empl);
        }

        // POST: Employeer/Edit/5
        [HttpPost]
        public ActionResult Edit(SE_RequestOnHoliday.Repository.DTO.EmployeerDTO employer)
        {

            if (ModelState.IsValid)
            {
                if (employer.Id != 0)
                {
                    repo.edit(employer);
                }
                else
                {
                    repo.create(employer);
                }

                db.SaveChanges();

                TempData["message"] = "Some message";
                return RedirectToAction("List");
            }


            TempData["message"] = "Some error message";
            return View(employer);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound();

            IEnumerable<RestVM> result = db.Rests.Join(db.Employers.Where(p => p.Id == id), // второй набор
                p => p.Id, // свойство-селектор объекта из первого набора
                c => c.Id, // свойство-селектор объекта из второго набора

                (p, c) => new RestVM()// результат
                {
                    Id = p.Id,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Name = string.Concat(c.FirtstName, " ", c.LastName),
                    Status = (Status)p.Status

                });
            ViewBag.EmployeerID = (int)id;
            return View(result);
        }

        public ActionResult Apply(int? id)
        {
            if (id == null)
                return HttpNotFound();

            string controller = RouteData.Values["id"].ToString();

            SelectList statusesList = new SelectList(restTypeRepo.list(), "Id", "Name");
            ViewBag.StatusesList = statusesList;

            if (id == null)
                return HttpNotFound();

            ViewBag.ID = id.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Apply(int EmployerID, UsersRequestsDTO usersRequestsDTO)
        {
            if (ModelState.IsValid)
            {
                restRepo.create(EmployerID, usersRequestsDTO.StartDate, usersRequestsDTO.EndDate, usersRequestsDTO.RestTypeId);
                restRepo.saveChanges();

                return RedirectToAction("List");
            }

            SelectList statusesList = new SelectList(restTypeRepo.list(), "Id", "Name");
            ViewBag.StatusesList = statusesList;
            return View();
        }

        [HttpPost]
        public ActionResult getRequest(string login, DateTime startDate, DateTime endDate)
        {
            if (ModelState.IsValid)
            {
                var empl = db.Employers.FirstOrDefault(p => p.Login == login);

                if (empl != null)
                {
                    Rest rest = new Rest() { StartDate = startDate, EndDate = endDate, Status = 1 };
                    rest.Employers.Add(empl);
                    db.Rests.Add(rest);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("List");

        }

        public JsonResult isLoginExistInDataBase(String userName)
        {
            return Json(repo.isLoginAvailable(userName), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();

            EmployeerDTO empl = repo.get((int)id);

            if (empl == null)
                return HttpNotFound();

            return View(empl);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            EmployeerDTO empl = repo.get((int)id);
            if (empl == null)
                return HttpNotFound();

            repo.delete(empl.Id);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult RestsList(int id)
        {
            return View();
        }

    }
}
