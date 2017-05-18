using SE_RequestOnHoliday.Models;
using SE_RequestOnHoliday.Repository.Abstract;
using SE_RequestOnHoliday.Repository.DTO;
using SE_RequestOnHoliday.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SE_RequestOnHoliday.Repository.Concrete
{
    public class EmployeerRepository : IEmployeeRepository
    {
        private EmployersContext db = new EmployersContext();

        public void create(EmployeerDTO employer)
        {
            var role = db.Roles.FirstOrDefault(i=> i.Id == employer.RoleId);
            SE_RequestOnHoliday.Models.Employer empl = new SE_RequestOnHoliday.Models.Employer()
            {
                FirtstName = employer.FirtstName,
                LastName = employer.LastName,
                Login = employer.Login,
                Password = employer.Password,
                Role = role
            };

            db.Employers.Add(empl);        
        }

        public void delete(int id)
        {
            //  Employer empl = new Employer { Id = id };
            //  db.Entry(b).State = EntityState.Deleted;

            Employer empl = db.Employers.FirstOrDefault(i => i.Id == id);
            db.Employers.Remove(empl);         
        }

        public void edit(EmployeerDTO employer)
        {
            var role = db.Roles.FirstOrDefault(i => i.Id == employer.RoleId);


            var empl = db.Employers.FirstOrDefault(i => i.Id == employer.Id);

            empl.FirtstName = employer.FirtstName;
            empl.LastName = employer.LastName;
            empl.Login = employer.Login;
            empl.Password = employer.Password;
            empl.Role = role;

            db.Entry(empl).State = EntityState.Modified;          
        }        

        public bool exist(int id)
        {
            return db.Employers.FirstOrDefault(p => p.Id == id) != null;
        }

        public bool isLoginAvailable(string login)
        {
            return db.Employers.FirstOrDefault(p => p.Login == login) != null;
        }

        public IEnumerable<EmployeerVM> list(int startPosition, int size)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            db.SaveChanges();
        }

        EmployeerDTO IEmployeeRepository.get(int id)
        {
             return (from p in db.Employers
                    where p.Id == id
                    select new EmployeerDTO
                    {
                        Id = p.Id,
                        FirtstName = p.FirtstName,
                        LastName = p.LastName,
                        Login = p.Login,
                        Password = p.Password,
                        RoleId = p.RoleId
                    }).FirstOrDefault();
        }

        IEnumerable<EmployeerVM> IEmployeeRepository.getAll()
        {
            IEnumerable<EmployeerVM> entries = db.Employers.Select(p => new EmployeerVM
            {
                Id = p.Id,
                FirstName = p.FirtstName,
                LastName = p.LastName
            }).ToList();

            return entries;
        }

    }
}