using DataAccesslayer.Models.Dashboards.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public interface IAdministratorDashboardCardsRepository
    {
        Task<int> GetTotalPatientsAsync();
        Task<int> GetTotalBedsAsync();
        Task<int> GetTotalWardsAsync();
        Task<int> GetTotalMaleEmployeesAsync();
        Task<int> GetTotalEmployeesAsync();
        Task<int> GetTotalFemaleEmployeesAsync();
        Task<int> GetTotalOtherEmployeesAsync();
        Task<int> GetTotalManagersAsync();
        Task<AdministratorDashboardCards> GetAdministratorDashboardCardsAsync();


    }
}
