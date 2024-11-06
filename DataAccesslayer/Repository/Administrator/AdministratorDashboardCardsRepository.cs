using DataAccesslayer.Models.Dashboards.Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public class AdministratorDashboardCardsRepository : IAdministratorDashboardCardsRepository
    {
        private readonly ISqlDataAccess _db;

        public AdministratorDashboardCardsRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<int> GetTotalBedsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalBeds", new { });
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalWardsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalWards", new { });
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalPatientsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalPatients", new { });
            return result.FirstOrDefault();
        }
        public async Task<int> GetTotalMaleEmployeesAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalMaleEmployees", new { });
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalEmployeesAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalEmployees", new { });
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalFemaleEmployeesAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalFemaleEmployees", new { });
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalOtherEmployeesAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalOtherEmployees", new { });
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalManagersAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetManagerCount", new { });
            return result.FirstOrDefault();
        }

        public async Task<AdministratorDashboardCards> GetAdministratorDashboardCardsAsync()
        {
            var result = new AdministratorDashboardCards
            {
                TotalBeds = await GetTotalBedsAsync(),
                TotalEmployees = await GetTotalEmployeesAsync(),
                TotalPatients = await GetTotalPatientsAsync(),
                TotalMaleEmployees = await GetTotalMaleEmployeesAsync(),
                TotalWards = await GetTotalWardsAsync(),
                TotalFemaleEmployees = await GetTotalFemaleEmployeesAsync(),
               TotalOtherEmployees = await GetTotalOtherEmployeesAsync(),
                //TotalMannagers = await GetTotalManagersAsync()
            };
            return result;
        }
    }

}
