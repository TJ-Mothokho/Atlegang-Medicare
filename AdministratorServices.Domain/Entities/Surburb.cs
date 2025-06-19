using AdministratorServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorServices.Domain.Entities
{
    public class Surburb : BaseModel
    {
        public string  Name { get; set; } = string.Empty;
        [ForeignKey("City")]
        public Guid CityID { get; set; }
        public string PostalCode { get; set; } = string.Empty;

        public ICollection<User>? Users { get; set; }
        public City City { get; set; }
    }
}
