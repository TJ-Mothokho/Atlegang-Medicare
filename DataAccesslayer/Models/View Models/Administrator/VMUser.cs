using DataAccesslayer.Models.Domains.Administrator;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.Administrator
{
    public class VMUser
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string SuburbName { get; set; }
        public string RoleName { get; set; }



        [StringLength(100)]
        public string LastName { get; set; }


        [StringLength(100)]
        public string FirstName { get; set; }


        [StringLength(50)]
        public string IDNumber { get; set; }


        public string Gender { get; set; }

        public IFormFile? ImageFile { get; set; } 



        [EmailAddress]
        [Required]
        public string Email { get; set; }


        [Phone]
        public string PhoneNumbers { get; set; }


        public int RoleID { get; set; }


        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        [StringLength(200)]
        public string ResidentialAddress { get; set; }


        public int SuburbID { get; set; }


        public string Status { get; set; }

    }
}
