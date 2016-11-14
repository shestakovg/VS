using _1C.V8.Data;
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
            if (allowProcess)
            {
                thread = new Thread(ProcessData);
                thread.Name = "Обработка пакетов с КПК";
                thread.Start();
            }
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
                        select top 30 h.id from orderHeader h with (nolock)
                                where exists (select * from dbo.orderDetail d with (nolock) where h.orderUUID = d.orderUUID and d.qty1>0 )
                                and sendTime between DATEADD(day,-2,getdate()) and  GETDATE() and h._send=0 and coalesce(h.orderType,0) in (1,0)";
            else
                cmd.CommandText = @"                                
                        select top 30 h.id from orderHeader h with (nolock)
                                where exists (select * from dbo.orderDetail d with (nolock) where h.orderUUID = d.orderUUID and d.qty2>0 )
                                and sendTime between DATEADD(day,-2,getdate()) and  GETDATE() and coalesce(h._send2,0) =0 and coalesce(h.orderType,0)=0";

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
             cmd.Dispose();
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
                ///  continue;
                try
                {
                    bool isAvaliableNewOrder = false;
                    SqlConnection connection = getConnection();
                    V8DbConnection _1cConnection = null;
                    long[] unprocOrders = getUnprocessOrder(connection, OrdersWH.MainWareHouse);
                    if (unprocOrders.Length > 0)
                    {
                        _1cConnection = _1CConnection.CreateAndOpenConnection();
                        if (_1cConnection == null) continue;
                    }

                    foreach (long id in unprocOrders)
                    {
                        isAvaliableNewOrder = true;
                        //  _1cConnection = _1CConnection.CreateAndOpenConnection();
                        if (_1cConnection == null) continue;
                        OrderWareHouse wh = new OrderWareHouse(id, connection, OrdersWH.MainWareHouse, _1cConnection);

                        if (wh.Initialized)
                        {
                            if (wh.prepare1CStructure())
                                if (wh.createOrder())
                                {
                                    wh.ApplyToSql();
                                    (wh as Order).Dispose();
                                    marcOrderProceed(connection, id, OrdersWH.MainWareHouse);
                                }
                        }
                        else
                        {
                            marcOrderProceed(connection, id, OrdersWH.MainWareHouse);
                        }
                       // wh.Dispose();
                        wh = null;
                        // _1CConnection.Close1CConnection(_1cConnection);
                    }
                    if (unprocOrders.Length > 0)
                        _1CConnection.Close1CConnection(_1cConnection);
                    //   GC.Collect();

                    long[] unprocOrders2 = getUnprocessOrder(connection, OrdersWH.ReatilWareHose);
                    if (unprocOrders2.Length > 0)
                    {
                        _1cConnection = _1CConnection.CreateAndOpenConnection();
                        if (_1cConnection == null) continue;
                    }
                    foreach (long id in unprocOrders2)
                    {
                        isAvaliableNewOrder = true;
                        // if (_1cConnection == null)

                        //  if (_1cConnection == null) break;
                        OrderWareHouse wh = new OrderWareHouse(id, connection, OrdersWH.ReatilWareHose, _1cConnection);
                        if (wh.Initialized)
                        {
                            if (wh.prepare1CStructure())
                                if (wh.createOrder())
                                {
                                    wh.ApplyToSql();
                                    (wh as Order).Dispose();
                                    marcOrderProceed(connection, id, OrdersWH.ReatilWareHose);
                                    wh = null;
                                }
                        }
                        else
                        {
                            marcOrderProceed(connection, id, OrdersWH.ReatilWareHose);
                        }
                      //  wh.Dispose();
                        wh = null;
                        //GC.Collect();
                    }
                    if (unprocOrders2.Length > 0)
                        _1CConnection.Close1CConnection(_1cConnection);

                    if (TradeServices.Classes.ClaimedPay.ExistsNewPays(connection))
                    {
                        //if (_1cConnection == null)
                        _1cConnection = _1CConnection.CreateAndOpenConnection();
                        if (_1cConnection != null)
                        {
                            TradeServices.Classes.ClaimedPay pays = new ClaimedPay(_1cConnection, connection);
                            pays.ProcessPays();
                            pays = null;
                        }
                        _1CConnection.Close1CConnection(_1cConnection);
                    }

                    connection.Close();
                    connection.Dispose();
                    //connection = null;
                    //if (_1cConnection != null)
                    //{
                    //    _1CConnection.Close1CConnection(_1cConnection);
                    //    //V8.ReleaseComObject(_1cConnection);
                    //   // _1cConnection = null;
                    //}

                    if (isAvaliableNewOrder)
                    {
                        //GC.WaitForPendingFinalizers();
                        GC.Collect();
                        //GC.Collect(1);
                        //GC.Collect(2);
                        // GC.WaitForFullGCComplete(500);
                    }
                }
                catch (Exception e)
                {
                    ProcessOrderLog log = new ProcessOrderLog(getConnection());
                    log.WriteLog(Guid.Empty, "Main thread exception: " + e.Message);

                }
                Thread.Sleep(3000);
            }
        }

    }
}