using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.SqlDataAccess;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public interface IAllergyRepository
    {
        Task<Allergy?> GetALLAllergybyIDAsync(int allergyID);
        Task<IEnumerable<Allergy>> GetAllAllergyAsync();
        Task<bool>AddAllergyAsync(Allergy allergy);

    }
}
