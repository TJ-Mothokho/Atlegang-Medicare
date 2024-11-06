using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
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
    public class NurseRepository : INurseRepository
    {
        private readonly ISqlDataAccess _data;

        public NurseRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<IEnumerable<VMNurse>> GetAllNurses()
        {
            return await _data.GetData<VMNurse, dynamic>("spGetNurses", new { });

        }


        public async Task<IEnumerable<VMNurse>> GetAllNurseSisters()
        {
            return await _data.GetData<VMNurse, dynamic>("spGetNurseSisters", new { });

        }

    }
}
