using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public class NoteRepository : INoteRepository
    {
        private readonly ISqlDataAccess _data;

        public NoteRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<bool> AddNoteAsync(Note note)
        {
            try
            {
                await _data.SaveData("spInsertNote", new { note.FileID, note.Description, note.Date });
                return true;
            }
            catch
            {
                return false;
            }
        }



        public async Task<IEnumerable<VMNote>> GetAllNotesAsync()
        {
            return await _data.GetData<VMNote, dynamic>("spGetPatientNotes", new { });
        }


       
            public async Task<IEnumerable<VMNote>> GetInstructionsAsync(int FileID)
            {

                var result = await _data.GetData<VMNote, dynamic>("spGetInstruction", new { FileID});

                return result;
            }
        
        public async Task<VMNote> GetNoteByIdAsync(int NoteID)
        {
            var result = await _data.GetData<VMNote, dynamic>("spGetPatientNoteByID", new { NoteID });
            return result.FirstOrDefault();
        }

        // Updated to use the Note model instead of VMNote
        public async Task<bool> UpdateNoteAsync(VMNote note)
        {
            try
            {
                await _data.SaveData("spUpdateNote", new { note.NoteID, note.FileID, note.Description, note.Date });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteNoteAsync(int NoteID)
        {
            try
            {
                await _data.SaveData("spDeleteNote", new { NoteID });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Note>> GetPatientNoteByFileID(int FileID)
        {
            var result = await _data.GetData<Note, dynamic>("spGetNoteByFileID", new { FileID });
            return result;
        }


        //testing

        public async Task<bool> RecordNoteAsync(Note note)
        {
            try
            {
                await _data.SaveData("spRecordNote", new { note.FileID, note.Description, note.Date });
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
