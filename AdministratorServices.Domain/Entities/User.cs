using AdministratorServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorServices.Domain.Entities
{
    public abstract class User : BaseModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumbers { get; set; } = string.Empty;
        [ForeignKey("Role")]
        public Guid RoleID { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        [ForeignKey("Surburb")]
        public Guid SurburbID { get; set; }

        public Role Role { get; set; }
        public Surburb Surburb { get; set; }
    }
}
