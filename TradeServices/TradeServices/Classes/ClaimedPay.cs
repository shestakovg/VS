using _1C.V8.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;

namespace TradeServices.Classes
{
    public class ClaimedPay
    {
        protected V8DbConnection _1cConnection;
        private SqlConnection connection;
        private ComObject stucture;

        public ClaimedPay(V8DbConnection con, SqlConnection connection)
        {
            this.connection = connection;
            this._1cConnection = con;
        }

        public static bool ExistsNewPays(SqlConnection connection)
        {
            const string sqlSelect = @"SELECT count(*)
                                      FROM [dbo].[ClaimedPays] with (nolock)
                                      where _send = 0";
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sqlSelect;
            Object res = cmd.ExecuteScalar();
            cmd.Dispose();
            if (Convert.ToInt32(res) > 0)
                return true;
            else
                return false;
        }

        public void ProcessPays()
        {
            const string sqlSelect = @"SELECT [id]
                                          ,[routeId]
                                          ,[payDate]
                                          ,[transactionId]
                                          ,[paySum]
                                      FROM [dbo].[ClaimedPays] with (nolock)
                                      where _send = 0";
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sqlSelect;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            foreach (DataRow row in tbl.Rows)
            {
                if (save1C(row))
                    markPay(Convert.ToInt64(row["id"]));
            }
            cmd.Dispose();
            da.Dispose();
            tbl.Dispose();
            da = null;
            cmd = null;
            tbl = null;
        }



        private bool save1C(DataRow row)
        {
            bool res = true;
            try
            {
                this.stucture = (ComObject)V8.Call(this._1cConnection, this._1cConnection.Connection, "externalGetNewStructure");
                V8.Call(this._1cConnection, this.stucture, "Вставить", new object[] { "Дата", Order.ПолучитьДату1СДляДокумента(Convert.ToDateTime(row["payDate"])) });
                V8.Call(this._1cConnection, this.stucture, "Вставить", new object[] { "Маршрут", row["routeId"].ToString() });
                V8.Call(this._1cConnection, this.stucture, "Вставить", new object[] { "Сделка", row["transactionId"].ToString() });
                V8.Call(this._1cConnection, this.stucture, "Вставить", new object[] { "СуммаОплаты", Convert.ToDecimal(row["paySum"]) });
                V8.Call(this._1cConnection, this._1cConnection.Connection, "SavePay", new object[] { this.stucture });
            }
            catch (Exception e)
            {
                res = false;
            }
            finally
            {
                if ( this.stucture != null)
                {
                   // this.stucture.Dispose();
                   // V8.ReleaseComObject(this.stucture);
                    this.stucture = null;
                }
            }
            return res;
        }

        private void markPay(long id)
        {
            using (SqlCommand cmd = this.connection.CreateCommand())
            {
                cmd.CommandText = "update ClaimedPays set _send = 1 where id = " + id.ToString();
                cmd.ExecuteNonQuery();
            }
        }

        
    }
}