using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorServices.Domain.Common
{
    public class BaseModel
    {
        [Key]
        protected Guid Id;
        protected DateTime CreatedAt;
        protected string Status;

        public BaseModel()
        {
            Id = new Guid();
            Status = "Active";
            CreatedAt = DateTime.Now;
        }
    }
}
