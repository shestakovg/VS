using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TradeServices.Models
{
    [Table("vLocationTraking")]
    public class ModelRouteTrip
    {
        [Key]
        [Column("id")]
        public Int64 Id { get; set;}
        [Column("routeId")]
        public Guid RouteId { get; set; }
        [Column("checkInTime")]
        public DateTime CheckInTime { get; set; }
        [Column("latitude")]
        public Double Latitude { get; set; }
        [Column("longtitude")]
        public Double Longtitude { get; set; }
    }

    [NotMapped]
    public class ModelRouteTripEx: ModelRouteTrip
    {
        public string CheckInTimeString { get; set; }
        public int RowNum { get; set; }
    }
}