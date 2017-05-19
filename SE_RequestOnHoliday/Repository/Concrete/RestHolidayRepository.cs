using SE_RequestOnHoliday.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SE_RequestOnHoliday.ViewModel;
using SE_RequestOnHoliday.Enums;
using SE_RequestOnHoliday.Models;
using SE_RequestOnHoliday.Repository.DTO;
using System.Data.Entity;

namespace SE_RequestOnHoliday.Repository.Concrete
{
    public class RestHolidayRepository : IRestHolidayRepository
    {
        private EmployersContext db = new EmployersContext();

        public void create(int employerID, DateTime startDate, DateTime endDate, int restTypeId)
        {
            RestType restType = db.RestTypes.FirstOrDefault(i=>i.Id == restTypeId);
            if (restType == null)
                return;

            Employer empl = db.Employers.FirstOrDefault(p => p.Id == employerID);

            if (empl != null)
            {
                Rest rest = new Rest() { StartDate = startDate, EndDate = endDate, Status = (int)Status.NotApplied , RestType =restType};
                rest.Employers.Add(empl);
                db.Rests.Add(rest);
            }
        }

        void IRestHolidayRepository.changeStatus(int restID, int statusID)
        {
            Rest rest = db.Rests.FirstOrDefault(i => i.Id == restID);
            if (rest != null)
            {
                rest.Status = statusID;
                db.Entry(rest).State = EntityState.Modified;
            }
        }

        IEnumerable<RestVM> IRestHolidayRepository.listByEmployer(int employeerID)
        {
            IEnumerable<RestVM> result = db.Rests.Join(db.Employers.Where(p => p.Id == employeerID), // второй набор
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

            return result;
        }

        public IEnumerable<RestVM> list()
        {
            IEnumerable<RestVM> result  = (from m in db.Rests
                      from emp in m.Employers
                      select  new RestVM()
                          {
                          Id = m.Id,
                          StartDate = m.StartDate,
                          EndDate = m.EndDate,
                          Name = string.Concat(emp.FirtstName, " ", emp.LastName),
                          Status = (Status)m.Status,
                          RestType = m.RestType.Name
                      });


            return result;
        }

        public IEnumerable<RestVM> list(int startPosition, int total)
        {
            IEnumerable<RestVM> result = db.Rests.Join(db.Employers.
               Skip(startPosition).Take(total), // второй набор
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

            return result;
        }

        public IEnumerable<RestVM> listByEmployer(int employeerID, int startPosition, int total)
        {
            IEnumerable<RestVM> result = db.Rests.Join(db.Employers.Where(p => p.Id == employeerID).
                Skip(startPosition).Take(total), // второй набор
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

            return result;
        }

        RestDTO IRestHolidayRepository.get(int restId)
        {
            Rest rest = db.Rests.FirstOrDefault(i => i.Id == restId);
            RestDTO restItem = new RestDTO() { RestId = rest.Id, StatusId = rest.Status };

            return restItem;
        }

        void IRestHolidayRepository.saveChanges()
        {
            db.SaveChanges();
        }
    }
}