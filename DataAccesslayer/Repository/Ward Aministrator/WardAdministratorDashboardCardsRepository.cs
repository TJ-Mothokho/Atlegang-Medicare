using DataAccesslayer.Models.Dashboards.Administrator;
using DataAccesslayer.Models.Dashboards.Ward_Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public class WardAdministratorDashboardCardsRepository : IWardAdministratorDashboardCardsRepository
    {
        private readonly ISqlDataAccess _db;

        public WardAdministratorDashboardCardsRepository(ISqlDataAccess db)
        {
            _db = db;
        }



        public async Task<WardAdminiStratorDashboardCards> GetAdminiStratorDashboardCardsAsync()
        {
            var result = new WardAdminiStratorDashboardCards
            {
                TotalOverallPatients = await GetTotalOverallPatientsAsync(),
                TotalBeds = await GetTatalBedsAsync(),
                TotalAdmittedPatients = await GetTotalAdmittedPatientsAsync(),
                TotalReferredPatients = await GetTotalReferredPatientsAync(),
                 TotalDischargePatients = await GetTotalDischargePatientsAsync(),
                TotalDischargedPerDay = await GetTotalDischargedPatientsPerDayAsync(),
                TotalAdmittedPatientsPerDay = await GetTotalAdmittedPatientsPerDayAsync(),
               TotalClosedFiles = await GetTotalClosedFilesAsync(),
            };
            return result;
        }

        public async Task<IEnumerable<dynamic>> GetAllDoctorsAsync()
        {
            return await _db.GetData<dynamic, dynamic>("spGetDoctors", new { });

        }

        public async Task<int> GetTatalBedsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalBeds", new { }); //Add storeprocedure
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalAdmittedPatientsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalAdmittedPatients", new { }); //Add storeprocedure
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalAdmittedPatientsPerDayAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalAdmittedandPerDayReport", new { }); //Add storeprocedure
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalClosedFilesAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalClosedFiles", new { }); //Add storeprocedure
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalDischargedPatientsPerDayAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalDischargePerDay", new { }); //Add storeprocedure
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalDischargePatientsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalDischargedPatients", new { }); //Add storeprocedure
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalOverallPatientsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalOverallPatients", new { }); //Add storeprocedure
            return result.FirstOrDefault();
        }

        public async Task<int> GetTotalReferredPatientsAync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalReferrals", new { }); //Add storeprocedure
            return result.FirstOrDefault();
        }


    }
}

