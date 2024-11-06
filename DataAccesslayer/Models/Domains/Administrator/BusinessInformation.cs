using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Administrator
{
    public class BusinessInformation
    {
        [Key]
        public int BusinessInfoID { get; set; }
        public string BusinessName { get; set; }
        public string Slogan { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string FacebookLink { get; set; }
        public string YouTubeLink { get; set; }
        public string MainLogo { get; set; }
        public string FooterLogo { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postal { get; set; }
        public string VAT_Number { get; set; }
        public string PhoneNumbers { get; set; }
    }
}
