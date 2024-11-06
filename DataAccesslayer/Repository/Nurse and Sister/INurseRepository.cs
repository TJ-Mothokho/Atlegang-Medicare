using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.Models.View_Models.Nurse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public interface INurseRepository
    {

        Task<IEnumerable<VMNurse>> GetAllNurses();

        Task<IEnumerable<VMNurse>> GetAllNurseSisters();

    }
}
