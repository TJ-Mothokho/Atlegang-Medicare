using DataAccesslayer.Models.Domains.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public interface IBedRepository
    {
        Task<bool> AddBedAsync(int WardID);
        Task<bool> DeleteBedAsync(int BedID);
        Task<bool> UpdateBedAsync(Bed bed);
        Task<Bed> GetBedByIdAsync(int BedID);
        Task<IEnumerable<Bed>> GetAllBedsAsync();
    }
}
