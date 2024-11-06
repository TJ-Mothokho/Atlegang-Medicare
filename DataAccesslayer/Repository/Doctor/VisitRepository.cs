using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Doctor
{
    public class VisitRepository : IVisitRepository
    {
        private readonly ISqlDataAccess _db;

        public VisitRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<bool> AddVisitAsync(Visit visit)
        {
            try
            {
                await _db.SaveData("spInsertVisit", new { visit.DoctorID, visit.FileID, visit.Title, visit.StartDate, visit.EndDate, visit.Description });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteVisitAsync(int visitID)
        {
            try
            {
                await _db.SaveData("spDeleteVisit", new { visitID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<dynamic>> GetAllVisitsAsync()
        {
            return await _db.GetData<dynamic, dynamic>("spGetVisits", new { });
        }
        public async Task<IEnumerable<Visit>> GetVisitByFileIdAsync(int FileID)
        {
            var result = await _db.GetData<Visit, dynamic>("spGetVisitByFileID", new { FileID });
            return result;
        }    
        
        public async Task<bool> UpdateVisitAsync(Visit visit)
        {
            try
            {
                await _db.SaveData("spUpdateVisit", new { visit.VisitID, visit.Title, visit.StartDate, visit.EndDate, visit.Description });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<VisitViewModel> GetVisitByIdAsync(int VisitID)
        {
            var result = await _db.GetData<VisitViewModel, dynamic>("spGetVisitById", new { VisitID });
            return result.FirstOrDefault();
        }
    }
}
