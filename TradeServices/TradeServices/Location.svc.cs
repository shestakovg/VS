using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TradeServices.Classes;
using TradeServices.DataEntitys.IncomingData;

namespace TradeServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Location" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Location.svc or Location.svc.cs at the Solution Explorer and start debugging.
    public class Location : ILocation
    {
        public void SaveCheckIn(OutletCheckIn[] checkInArray)
        {
            string cmdText = @"INSERT INTO [dbo].[OutletCheckIn]
                                   ([routeId]
                                   ,[outletId]
                                   ,[checkInTime]
                                   ,[latitude]
                                   ,[longtitude])
                             VALUES
                                   (@routeId
                                   ,@outletId
                                   ,@checkInTime
                                   ,@latitude
                                   ,@longtitude)";
            SqlConnection con = SaveData.getConnection();
            
            foreach(OutletCheckIn checkIn in checkInArray)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("routeId", new Guid(checkIn.routeId));
                cmd.Parameters.AddWithValue("outletId", new Guid(checkIn.outletId));
                cmd.Parameters.AddWithValue("checkInTime", checkIn.sateliteTime);
                cmd.Parameters.AddWithValue("latitude", Convert.ToDecimal(checkIn.latitude));
                cmd.Parameters.AddWithValue("longtitude", Convert.ToDecimal(checkIn.longtitude));
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            con.Close();
        }
    }
}

