using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.Models
{
    [DataContract]
    [Table("ApprovedOutletLocation")]
    public class KnownOutletLocation
    {
        [Key]
        [Column("outletId")]
        [DataMember]
        public Guid OutletId { get; set;}
        [Column("latitude")]
        [DataMember]
        public Double Latitude { get; set; }
        [Column("longtitude")]
        [DataMember]
        public Double Longtitude { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is KnownOutletLocation))
                return false;
            else
            {
                if (this.OutletId == (obj as KnownOutletLocation).OutletId)
                    return true;
                else
                    return false;
            }
                            
        }
    }



    //public class KnownOutletLocation
    //{
    //    public Guid OutletId { get; set; }
    //    public Double Latitude { get; set; }
    //    public Double Longtitude { get; set; }
    //}
}