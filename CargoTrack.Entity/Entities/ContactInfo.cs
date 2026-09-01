using CargoTrack.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.Entity.Entities
{
    public class ContactInfo:BaseEntity
    {
        
        public string Adress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
