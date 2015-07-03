using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using TradeServices.DataEntitys;

namespace TradeServices.Classes
{
    public class ProcessIncomingData
    {
        private static bool allowProcess = false;
        private static Thread thread;
        
        public static void Start()
        {

            allowProcess = Convert.ToBoolean(ConfigurationManager.AppSettings["processOrders"]);
            thread = new Thread(ProcessData);
            thread.Name = "Обработка пакетов с КПК";
            thread.Start();
        }

        public static void Stop()
        {
            if (thread != null)
                if (thread.IsAlive)
                {
                    lock ((allowProcess as object))
                    {
                        allowProcess = false;
                    }
                    thread.Join();
                }
        }

        private static long[] getUnprocessOrder(SqlConnection con, OrdersWH wh)
        {
            long[] resultArray = new long[] { };
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            if (wh == OrdersWH.MainWareHouse) 
                cmd.CommandText = @"                                
                        select top 10 h.id from orderHeader h with (nolock)
                                where exists (select * from dbo.orderDetail d with (nolock) where h.orderUUID = d.orderUUID and d.qty1>0 )
                                and sendTime between DATEADD(day,-2,getdate()) and  GETDATE() and h._send=0 ";
            else
                cmd.CommandText = @"                                
                        select top 10 h.id from orderHeader h with (nolock)
                                where exists (select * from dbo.orderDetail d with (nolock) where h.orderUUID = d.orderUUID and d.qty2>0 )
                                and sendTime between DATEADD(day,-2,getdate()) and  GETDATE() and coalesce(h._send2,0) =0 ";

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                List<long> list = new List<long>();
                while (reader.Read())
                {
                    list.Add(reader.GetInt64(0));
                }
                resultArray = list.ToArray();
            }
            reader.Close();
            return resultArray;
        }
        private static void marcOrderProceed(SqlConnection con, long id, OrdersWH wh)
        {
             SqlCommand cmd = con.CreateCommand();
             cmd.CommandType = System.Data.CommandType.Text;
            if (wh == OrdersWH.MainWareHouse)
             cmd.CommandText = @"update dbo.orderHeader
                                    set _send = 1  where id = "+id.ToString();
            else
                cmd.CommandText = @"update dbo.orderHeader
                                    set _send2 = 1  where id = " + id.ToString();
             cmd.ExecuteNonQuery();
        }
        private static SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            con.Open();
            return con;
        }
        public static void ProcessData()
        {
            while (allowProcess)
            {
                bool isAvaliableNewOrder = false;
                SqlConnection connection = getConnection();
                foreach (long id in getUnprocessOrder(connection, OrdersWH.MainWareHouse))
                {
                    isAvaliableNewOrder = true;
                    OrderWareHouse wh = new OrderWareHouse(id, connection, OrdersWH.MainWareHouse);
                    
                    if (wh.Initialized)
                    {
                        if (wh.prepare1CStructure())
                            if (wh.createOrder())
                            {
                                wh.ApplyToSql();
                                wh.Dispose();
                                marcOrderProceed(connection, id, OrdersWH.MainWareHouse);
                            }
                    }
                    else
                    {
                        marcOrderProceed(connection, id, OrdersWH.MainWareHouse);
                    }
               }
            //   GC.Collect();
            

                foreach (long id in getUnprocessOrder(connection, OrdersWH.ReatilWareHose))
                {
                    isAvaliableNewOrder = true;
                    OrderWareHouse wh = new OrderWareHouse(id, connection, OrdersWH.ReatilWareHose);
                    if (wh.Initialized)
                    {
                        if (wh.prepare1CStructure())
                            if (wh.createOrder())
                            {
                                wh.ApplyToSql();
                                wh.Dispose();
                                marcOrderProceed(connection, id, OrdersWH.ReatilWareHose);
                                wh = null;
                            }
                    }
                    else
                    {
                        marcOrderProceed(connection, id, OrdersWH.ReatilWareHose);
                    }

                    
                    //GC.Collect();
                }

                connection.Close();
                connection.Dispose();
                
                if (isAvaliableNewOrder)
                {
                    GC.Collect();
                   // GC.WaitForFullGCComplete(500);
                }
                Thread.Sleep(500);
            }
        }

    }
}