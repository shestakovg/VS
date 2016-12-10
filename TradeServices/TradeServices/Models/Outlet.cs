using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TradeServices.Models
{
    [Table("v1cOutlets")]
    public class Outlet
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("customerId")]
        public Guid CustomerId { get; set; }
        [Column("Description")]
        public String Description { get; set; }
        [Column("Address")]
        public String Address { get; set; }
        [Column("isDeleted")]
        public bool isDeleted { get; set; }
    }
}