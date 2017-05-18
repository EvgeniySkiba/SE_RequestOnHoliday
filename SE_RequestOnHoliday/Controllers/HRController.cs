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

namespace SE_RequestOnHoliday.Controllers
{

    [Authorize(Roles = "Admin, HR")]
    public class HRController : Controller
    {
        IRestHolidayRepository restRepo;

        public HRController()
        {
            restRepo = new RestHolidayRepository();
        }

        private EmployersContext db = new EmployersContext();

       
        public ActionResult Index()
        {
            IEnumerable<RestVM> entries = restRepo.list();
         
            return View(entries);
        }

        public ActionResult ChangeStatus(int? id)
        {
            if (id == null)
                return HttpNotFound();

            RestDTO rest = restRepo.get((int)id);

             return View(rest);
        }

        [HttpPost]
        public ActionResult ChangeStatus(int restID, int StatusId)
        {

            restRepo.changeStatus(restID, StatusId);
            restRepo.saveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}