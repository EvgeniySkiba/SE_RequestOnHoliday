using SE_RequestOnHoliday.Repository.DTO;
using SE_RequestOnHoliday.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.Repository.Abstract
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeerVM> getAll();

        IEnumerable<EmployeerVM> list(int startPosition, int size);

        EmployeerDTO get(int id);

        void edit(EmployeerDTO employyer);

        void delete(int id);

        bool exist(int id);

        bool isLoginAvailable(string login);

        void create(EmployeerDTO empployyer);

        void save();

    }
}