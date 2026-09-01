using CargoTrack.Entity.Entities.Common;
using CargoTrack.Entity.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.Entity.Entities
{
    public class Cargo:BaseEntity
    {
        public string TrackCode { get; set; }
        public DateTime ShipmentDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public double Weight { get; set; }        
        public CargoType CargoType { get; set; }

        public CargoStatus CargoStatus { get; set; }

        public Guid SenderId { get; set; }    
        public Guid ReceiverId { get; set; }     

        public Guid OriginBranchId { get; set; }
        public Guid DestinationBranchId { get; set; }

        //Navigation Properties
        public virtual AppUser Sender { get; set; }
        public virtual AppUser Receiver { get; set; }
        public virtual Branch OriginBranch { get; set; }
        public virtual Branch DestinationBranch { get; set; }
    }
}
