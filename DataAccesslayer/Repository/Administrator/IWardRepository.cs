using DataAccesslayer.Models.Domains.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public interface IWardRepository
    {
        Task<bool> AddWardAsync(Ward ward);
        Task<bool> DeleteWardAsync(int WardID);
        Task<bool> UpdateWardAsync(Ward ward);
        Task<Ward> GetWardByIdAsync(int WardID);
        Task<IEnumerable<Ward>> GetAllWardsAsync();
    }
}
