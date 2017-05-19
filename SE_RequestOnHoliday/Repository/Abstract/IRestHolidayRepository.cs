using SE_RequestOnHoliday.Repository.DTO;
using SE_RequestOnHoliday.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE_RequestOnHoliday.Repository.Abstract
{
    public interface IRestHolidayRepository
    {
        void create(int employerID, DateTime startDate, DateTime endDate, int restTypeId);

        void changeStatus(int restID, int statusID);

        IEnumerable<RestVM> list();

        IEnumerable<RestVM> list(int startPosition, int total);

        IEnumerable<RestVM> listByEmployer(int employeerID);

        IEnumerable<RestVM> listByEmployer(string login);

        IEnumerable<RestVM> listByEmployer(int employeerID, int startPosition, int total);

        RestDTO get(int restId);
        void saveChanges();

    }
}