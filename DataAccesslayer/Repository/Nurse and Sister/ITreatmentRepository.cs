using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public interface ITreatmentRepository
    {
        Task<bool> AddAsync(Treatment treatment);

        Task<bool> UpdateAsync(Treatment treatment);

        Task<bool> DeleteAsync(int treatmentId);

        Task<Treatment> GetByIdAsync(int treatmentId);

        Task<IEnumerable<Treatment>> GetAllAsync();


    }
}
