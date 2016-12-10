using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TradeServices.Models
{
    [Table("v1cRoutes")]
    public class Route
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("parentId1")]
        public Guid ParentId { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("isDeleted")]
        public bool isDeleted { get; set; }
        [Column("isFolder")]
        public bool isFolder { get; set; }
    }
}