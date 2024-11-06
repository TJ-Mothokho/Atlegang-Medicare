using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Dashboards.Nurse;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public interface INoteRepository
    {
        Task<VMNote> GetNoteByIdAsync(int NoteID);
        Task<bool> AddNoteAsync(Note note);
        Task<bool> UpdateNoteAsync(VMNote note);
        Task<IEnumerable<VMNote>> GetAllNotesAsync();
        Task<bool> DeleteNoteAsync(int NoteID);
        Task<IEnumerable<VMNote>> GetInstructionsAsync(int FileID);
        Task<IEnumerable<Note>> GetPatientNoteByFileID(int FileID);


        //testing

        Task<bool> RecordNoteAsync(Note note);


    }
}
