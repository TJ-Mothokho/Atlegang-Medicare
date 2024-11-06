using DataAccesslayer.Models.Dashboards.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Doctor
{
    public interface IDoctorDashboardCardsRepository
    {
        Task<int> GetTotalPatientFilesAsync();
        Task<int> GetTotalVisitsAsync();
        Task<int> GetTotalFilesDischargedAsync();
        Task<int> GetTotalReferralsAsync();
        Task<int> GetTotalMalePatientsAsync();
        Task<int> GetTotalFemalePatientsAsync();
        Task<int> GetTotalOtherPatientsAsync();
        Task<DoctorDashboardCards> GetDoctorDashboardCardsAsync();
    }
}
