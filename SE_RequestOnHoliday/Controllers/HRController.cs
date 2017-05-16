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
    [Authorize(Roles = "Admin, HR")]
    public class HRController : Controller
    {

        private EmployersContext db = new EmployersContext();

        /// <summary>
        /// все заявки 
        /// </summary>
        /// <returns></returns>
        //  [Authorize]
        public ActionResult Index()
        {
            /* IEnumerable<RestVM> result = db.Rests.Join(db.Employers, // второй набор
                 p => p.ChangeStatusId, // свойство-селектор объекта из первого набора
                 c => c.ChangeStatusId, // свойство-селектор объекта из второго набора
                 (p, c) => new RestVM // результат
                 {
                     ChangeStatusId = p.ChangeStatusId,
                     StartDate = p.StartDate,
                     EndDate = p.EndDate,
                     Name = String.Concat(c.LastName, " " + c.FirtstName)

                 }).ToList();*/
            IEnumerable<RestVM> entries = db.Rests.Select(p => new RestVM
            {
                Id = p.Id,
                Name = p.Employers.FirstOrDefault().LastName,
                StartDate = p.StartDate,
                EndDate = p.StartDate,
                Status = p.Status
            }).ToList();        

            return View(entries);
        }

        public ActionResult ChangeStatus(int id)
        {
            ChangeStatus currentStatus = (from c in db.Rests
                                         where c.Id == id
                                         select new ChangeStatus
                                         {
                                             ChangeStatusId = c.Id,
                                             Status = c.Status
                                         }).FirstOrDefault();

                
            return View(currentStatus);
        }

        [HttpPost]
        public ActionResult ChangeStatus(int ChangeStatusId, int Status)
        {
            Rest rest = db.Rests.FirstOrDefault(p=>p.Id == ChangeStatusId);

            if (rest != null)
            {
                rest.Status = Status;
                db.Entry(rest).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}