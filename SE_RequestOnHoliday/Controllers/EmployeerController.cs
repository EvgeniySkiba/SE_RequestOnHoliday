using SE_RequestOnHoliday.Models;
using SE_RequestOnHoliday.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE_RequestOnHoliday.Controllers
{
    [Authorize(Roles = "Admin, User, HR")]
    public class EmployeerController : Controller
    {
        EmployersContext db = new EmployersContext();
        // GET: Employeer
        public ActionResult List()
        {
            IEnumerable<EmployeerVM> entries = db.Employers.Select(p => new EmployeerVM
            {
                Id = p.Id,
                FirstName = p.FirtstName,
                LastName = p.LastName
            }).ToList();

            return View(entries);
        }



        // GET: Employeer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employeer/Create
        [HttpPost]
        public ActionResult Create(Employer employee)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    // TODO: Check exist login
                    var exist = db.Employers
                         .FirstOrDefault(p => p.Login == employee.Login) != null;

                    if (exist)
                    {
                        TempData["message"] = "Указанный логин занят";
                        return View();
                    }
                    // TODO: implements drop down lost 
                    //TODO: for test                
                    employee.Role = db.Roles.FirstOrDefault(p => p.Id == 1);
                    db.Employers.Add(employee);
                    db.SaveChanges();

                    return RedirectToAction("List");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Employeer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();

            EmployeerVM empl = (from p in db.Employers
                                where p.Id == id
                                select new EmployeerVM
                                {
                                    Id = p.Id,
                                    FirstName = p.FirtstName,
                                    LastName = p.LastName
                                }).FirstOrDefault();

            if (empl == null)
                return HttpNotFound();

            return View(empl);
        }

        // POST: Employeer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employer employer)
        {
           
            if (ModelState.IsValid)
            {
                if (employer.Id != 0)
                {
                    db.Entry(employer).State = EntityState.Modified;
                }
                else
                {
                    db.Employers.Add(employer);
                }
                db.SaveChanges();
                TempData["message"] = "Some message";
                return RedirectToAction("Index");
            }
            else { }


            TempData["message"] = "Some error message";

            return View(employer);


        }
        
        public ActionResult Details(string login)
        {
            IEnumerable<RestVM> result = db.Rests.Join(db.Employers.Where(p => p.Login == login), // второй набор
                p => p.Id, // свойство-селектор объекта из первого набора
                c => c.Id, // свойство-селектор объекта из второго набора

                (p, c) => new RestVM()// результат
                {
                    Id = p.Id,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Name = string.Concat(c.FirtstName, " ", c.LastName)
                });

            return View(result);
        }

        public ActionResult getRequest(string login = "test")
        {
            ViewBag.Login = login;
            return View(new RequestVM());
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
    }
}
