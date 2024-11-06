using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister
{
    public class Note
    {
        public int NoteID { get; set; }
        public int FileID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
