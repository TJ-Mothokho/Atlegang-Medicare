using DataAccesslayer.Models.Dashboards.Doctor;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Doctor
{
    public class DoctorDashboardCardsRepository :IDoctorDashboardCardsRepository
    {
        private readonly ISqlDataAccess _db;

        public DoctorDashboardCardsRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        // Retrieves the total number of patient files in the system
        public async Task<int> GetTotalPatientFilesAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalPatientsFiles", new { });
            return result.FirstOrDefault();
        }

        // Retrieves the total number of visits in the system
        public async Task<int> GetTotalVisitsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalVisits", new { });
            return result.FirstOrDefault();
        }

        // Retrieves the total number of discharged files from the system
        public async Task<int> GetTotalFilesDischargedAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalRemoved", new { });
            return result.FirstOrDefault();
        }

        // Retrieves the total number of referrals in the system
        public async Task<int> GetTotalReferralsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalReferrals", new { });
            return result.FirstOrDefault();
        }

        // Retrieves the total number of male patients in the system
        public async Task<int> GetTotalMalePatientsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalMalePatients", new { });
            return result.FirstOrDefault();
        }

        // Retrieves the total number of female patients in the system
        public async Task<int> GetTotalFemalePatientsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalFemalePatients", new { });
            return result.FirstOrDefault();
        }

        // Retrieves the total number of patients categorized as other in the system
        public async Task<int> GetTotalOtherPatientsAsync()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalOtherPatients", new { });
            return result.FirstOrDefault();
        }

        // Retrieves all the necessary data for the Doctor's dashboard cards, consolidating multiple metrics
        public async Task<DoctorDashboardCards> GetDoctorDashboardCardsAsync()
        {
            var result = new DoctorDashboardCards
            {
                TotalPatientsFiles = await GetTotalPatientFilesAsync(),           // Total number of patient files
                TotalVisits = await GetTotalVisitsAsync(),                       // Total number of visits
                TotalDischargedPatients = await GetTotalFilesDischargedAsync(),  // Total number of discharged patients
                TotalReferredPatients = await GetTotalReferralsAsync(),          // Total number of referrals
                TotalFemalePatients = await GetTotalFemalePatientsAsync(),       // Total number of female patients
                TotalMalePatients = await GetTotalMalePatientsAsync(),           // Total number of male patients
                TotalOtherPatients = await GetTotalOtherPatientsAsync(),         // Total number of patients categorized as other
            };
            return result; // Returns the aggregated data for the Doctor's dashboard cards
        }
    }
}
