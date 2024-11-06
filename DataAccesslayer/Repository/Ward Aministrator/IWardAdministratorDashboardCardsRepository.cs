using DataAccesslayer.Models.Dashboards.Ward_Administrator;
using DataAccesslayer.Repository.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public interface IWardAdministratorDashboardCardsRepository 

    {
        Task<IEnumerable<dynamic>> GetAllDoctorsAsync();
        Task<int> GetTatalBedsAsync();
        Task <int> GetTotalAdmittedPatientsAsync();
        Task<int> GetTotalOverallPatientsAsync();
         Task<int> GetTotalReferredPatientsAync();
        Task<int> GetTotalAdmittedPatientsPerDayAsync();
        Task<int> GetTotalDischargedPatientsPerDayAsync();
        Task<int> GetTotalDischargePatientsAsync();
        Task<int> GetTotalClosedFilesAsync();
        Task<WardAdminiStratorDashboardCards> GetAdminiStratorDashboardCardsAsync();
    }
}
