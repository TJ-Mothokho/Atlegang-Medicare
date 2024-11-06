using DataAccesslayer.Models.Dashboards.Doctor;
using DataAccesslayer.Models.Dashboards.Nurse;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public class NurseDashboardCardsRepository : INurseDashboardCardsRepository
    {
        private readonly ISqlDataAccess _data;

        public NurseDashboardCardsRepository(ISqlDataAccess data)
        {
            
            _data = data;
        }

        public async Task<int> GetAllNewAdmissionsAsync()
        {
            var result = await _data.GetData<int, dynamic>("spGetNewAdmittedPatients", new { });
            return result.FirstOrDefault();
        }


        public async Task<int> GetTotalAdmittedPatientsAsync()
        {
            var result = await _data.GetData<int, dynamic>("spGetAllAdmittedPatients", new { });
            return result.FirstOrDefault();


        }

        public async Task<int> GetTotalVisitsAsync()
        {
            var result = await _data.GetData<int, dynamic>("spGetTotalVisits", new { });
            return result.FirstOrDefault();

        }

        public async Task<int> GetAllPatientsWithPrescriptionsAsync()
        {
            var result = await _data.GetData<int, dynamic>("spGetTotalPatientsScripts", new { });
            return result.FirstOrDefault();

        }

     

     

        public async Task<NurseDashboard> GetNurseDashboardAsync()
        {
            var result = new NurseDashboard
            {
                TotalNewAdmissions = await GetAllNewAdmissionsAsync(),
                TotalPatientsAdmitted = await GetTotalAdmittedPatientsAsync(), 
                TotalVisits = await GetTotalVisitsAsync(),
                TotalPatientsPrescriptions= await GetAllPatientsWithPrescriptionsAsync()
            };
            return result;
        }

    }
}
