using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Doctor
{
    public interface IVisitRepository
    {
        Task<IEnumerable<dynamic>> GetAllVisitsAsync();
        Task<VisitViewModel> GetVisitByIdAsync(int VisitID);
        Task<IEnumerable<Visit>> GetVisitByFileIdAsync(int FileID);
        Task<bool> AddVisitAsync(Visit visit);
        Task<bool> UpdateVisitAsync(Visit visit);
        Task<bool> DeleteVisitAsync(int id);
    }
}
