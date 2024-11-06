using DataAccesslayer.Models.Domains.Ward_Administrator;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace Atlegang_Medicare.ViewModels
{
    public class VMPatient
    {
        // User fields
        public int UserID { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        public string PatientName { get; set; } 

        [StringLength(50)]
        public string IDNumber { get; set; }

        public string Gender { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }


        [Phone]
        public string PhoneNumbers { get; set; }


        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(200)]
        public string ResidentialAddress { get; set; }

        public int SuburbID { get; set; }
        public string SuburbName { get; set; }
        public int RoleID { get; set; }

        public string Status { get; set; }

        // Patient-specific fields
        public int PatientID { get; set; }
        public List<int> SelectedAllergies { get; set; } = new List<int>();  // For checkboxes
        public List<int> SelectedChronicConditions { get; set; } = new List<int>();  // For checkboxes
        public IEnumerable<SelectListItem> Allergies { get; set; }  // To populate the checkbox list
        public IEnumerable<SelectListItem> ChronicConditions { get; set; }  // To populate the checkbox list
        public IEnumerable<PatientCondition> Medication { get; set; }
    }
}
