using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccesslayer.Models.Domains.Administrator
{
    public class User
    {
        [Key]
        public int UserID { get; set; }


        [StringLength(100)]
        public string LastName { get; set; }


        [StringLength(100)]
        public string FirstName { get; set; }


        [StringLength(50)]
        public string IDNumber { get; set; }


        public string Gender { get; set; }



        [EmailAddress]
        [Required]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }


        [Phone]
        public string PhoneNumbers { get; set; }


        public int RoleID { get; set; }


        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } 


        [StringLength(200)]
        public string ResidentialAddress { get; set; }


        public int SuburbID { get; set; }


        public string Status { get; set; }

        //public Role Role { get; set; }
    }
}
