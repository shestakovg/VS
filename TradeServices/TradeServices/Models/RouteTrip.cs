using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TradeServices.Models
{
    [Table("RouteTrip")]
    public class RouteTrip
    {
        //[Column(Order = 0), Key]
        //[Column("RowNum")]
        //public int RowNum { get; set; }
        [Column("routeId", Order = 0), Key]
        public Guid RouteId { get; set; }
        [Column("routeDate", Order = 1), Key]
        public DateTime RouteDate { get; set; }
        [Column("distance")]
        public Double? Distance { get; set; }
        [Column("distanceBetweenCheckIn")]
        public Double? DistanceBetweenCheckIn { get; set; }
        [Column("minCheckInTime")]
        public DateTime? MinCheckInTime { get; set; }
        [Column("maxCheckInTime")]
        public DateTime? MaxCheckInTime { get; set; }
        [Column("firstOutlet")]
        public Guid? FirstOutlet { get; set; }
        [Column("lastOutlet")]
        public Guid? LastOutlet { get; set; }
    }
}