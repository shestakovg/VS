using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TradeServices.Classes
{
    public class ProcessOrderLog
    {
        private SqlConnection con;

        public ProcessOrderLog(SqlConnection con)
        {
            this.con = con;
        }

        public void WriteLog(Guid orderUUD, string description)
        {
            SqlCommand cmd = this.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"INSERT INTO [dbo].[orderLog]
                                               ([orderUUID]
                                               ,[processTime]
                                               ,[_description])
                                VALUES
                                               (@orderUUID
                                               ,@processTime
                                               ,@_description)";
            cmd.Parameters.AddWithValue("orderUUID", orderUUD);
            cmd.Parameters.AddWithValue("_description", description);
            cmd.Parameters.AddWithValue("processTime",DateTime.Now);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            cmd.Dispose();
            cmd = null;
        }

    }
}