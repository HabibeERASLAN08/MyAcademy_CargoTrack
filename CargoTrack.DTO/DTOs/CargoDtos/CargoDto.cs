using CargoTrack.Entity.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.DTO.DTOs.CargoDtos
{
    public class CargoDto
    {
        public Guid Id { get; set; }    
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
    }
}
