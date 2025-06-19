using AdministratorServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorServices.Domain.Entities
{
    public class City : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        [ForeignKey("Province")]
        public Guid ProvinceID { get; set; }
        

        public ICollection<Surburb>? Surburbs { get; set; }
        public Province Province { get; set; }
    }
}
