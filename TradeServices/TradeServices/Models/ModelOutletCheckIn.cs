using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TradeServices.Models
{
    [Table("vOutletCheckIn")]
    public class ModelOutletCheckIn
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("routeId")]
        public Guid RouteId { get; set; }
        [Column("outletId")]
        public Guid OutletId { get; set; }
        [Column("checkInTime")]
        public DateTime CheckInTime { get; set; }
        [Column("latitude")]
        public Double Latitude { get; set; }
        [Column("longtitude")]
        public Double Longtitude { get; set; }

    }

    [NotMapped]
    public class ModelOutletCheckInEx : ModelOutletCheckIn
    {
        public string Outlet { get; set; }
    }
}