using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public interface IVitalRepository
    {
        Task<bool> AddVital(Vital vital);

        Task<bool> UpdateVital(Vital vital);

        Task<bool> DeleteVital(int vitalId);

         Task<Vital> GetVitalById(int vitalId);

        Task<IEnumerable<Vital>> GetAllVitals();

    }
}
