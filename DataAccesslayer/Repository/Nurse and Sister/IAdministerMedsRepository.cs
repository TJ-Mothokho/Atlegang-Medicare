using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Nurse;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{

    public interface IAdministerMedsRepository
    {
        Task<bool> AddAdministerMeds(MedsAdminister meds);

        Task<bool> UpdateMedicationAdministered(VMAdministerMeds meds);

        Task<bool> DeleteMedicationAdministered(int MedsAdministerID);

        Task<IEnumerable<VMAdministerMeds>> GetMedsAdministeredByFileId(int FileID );

        Task<VMAdministerMeds> GetMedsAdministeredById(int medsAdministerID);

        //Task<VMAdministerMeds> GetMedsAdministeredByRole(string Type);

        //Testing
        //Task<IEnumerable<dynamic>> GetMedsAdministeredByNursingSister();

        Task<IEnumerable<VMAdministerMeds>> GetAllMedsAdministeredByNurse();

        Task<int> GetMedsAdministerCountAsync(int fileId, int medicationID);



    }


}
