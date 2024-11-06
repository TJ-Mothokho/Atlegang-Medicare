using DataAccesslayer.Models.Dashboards.Nurse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public interface INurseDashboardCardsRepository
    {

        Task<int> GetAllNewAdmissionsAsync();
        Task<int> GetAllPatientsWithPrescriptionsAsync();
        Task<int> GetTotalAdmittedPatientsAsync();
        Task<int> GetTotalVisitsAsync();
        Task<NurseDashboard> GetNurseDashboardAsync();
    }
}
