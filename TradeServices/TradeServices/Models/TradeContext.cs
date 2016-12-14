using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TradeServices.Models
{
    public class TradeContext : DbContext
    {
        public TradeContext() :base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Route> Routes { get; set; }
        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<RouteTrip> RouteTrips { get; set; }
        public DbSet<ModelOutletCheckIn> CheckIns { get; set; }
        public DbSet<ModelRouteTrip> Trips { get; set; }

    }
}