using AdministratorServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorServices.Domain.Entities
{
    public class Province : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        [ForeignKey("Country")]
        public Guid CountryID { get; set; }

        public ICollection<City>? Cities { get; set; }
        public Country Country { get; set; }
    }
}
