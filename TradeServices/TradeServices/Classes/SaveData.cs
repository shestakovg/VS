using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TradeServices.Classes.IncomingData;
using System.Data;
using TradeServices.DataEntitys.IncomingData;
using TradeServices.DataEntitys;
using System.Diagnostics;

namespace TradeServices.Classes
{

    public class SaveData
    {
        public static SqlConnection getConnection()
        {
            SqlConnection con= new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            con.Open();
            return con;
        }

        public static SqlConnection get1cConnection()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection1CString"]);
            con.Open();
            return con;
        }

        public static bool SaveHeader(OrderHeader header)
        {
            #region Sql Statement
            const string sqlInsert = @"
                                        INSERT INTO [dbo].[orderHeader]
                                                    ([orderUUID]
                                                    ,[outletId]
                                                    ,[orderDate]
                                                    ,[deliveryDate]
                                                    ,[orderNumber]
                                                    ,[notes]
                                                    ,[payType]
                                                    ,[autoLoad]
                                                    ,[_send]
                                                    ,[sendTime]
                                                    ,[routeId], _send2,orderType)
                                                VALUES
                                                    (@orderUUID
                                                    ,@outletId
                                                    ,convert(datetime,@orderDate,101)
                                                    ,convert(datetime,@deliveryDate,101)
                                                    ,@orderNumber
                                                    ,@notes
                                                    ,@payType
                                                    ,@autoLoad
                                                    ,-1
                                                    ,getdate()
                                                    ,@routeId, -1, @orderType)";
            const string sqlUpdate = @"UPDATE [dbo].[orderHeader]
                                           SET 
                                              [orderDate] = convert(datetime,@orderDate,101)
                                              ,[deliveryDate] = convert(datetime,@deliveryDate,101)
                                              ,[orderNumber] = @orderNumber
                                              ,[notes] = @notes
                                              ,[payType] = @payType
                                              ,[autoLoad] = @autoLoad
                                              ,[_send] = -1
                                              ,[_send2] = -1
                                              ,[sendTime] = getdate()
                                              ,[routeId] = @routeId
                                              ,orderType = @orderType
                                         WHERE id = @id";
            const string sqlSearchOrder = @"select id from [dbo].[orderHeader] where [orderUUID]=@orderUUID";
            #endregion
            bool result = true;
            SqlConnection con = getConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlSearchOrder;
            cmd.Parameters.AddWithValue("orderUUID", new Guid(header.orderUUID));

            SqlCommand cmdDml = con.CreateCommand();
            cmdDml.CommandType = CommandType.Text;
            cmdDml.Parameters.AddWithValue("orderUUID", new Guid(header.orderUUID));
            cmdDml.Parameters.AddWithValue("outletId", new Guid(header.outletId));
            cmdDml.Parameters.AddWithValue("orderDate", header.orderDate);
            cmdDml.Parameters.AddWithValue("deliveryDate", header.deliveryDate);
            cmdDml.Parameters.AddWithValue("orderNumber", header.orderNumber);
            cmdDml.Parameters.AddWithValue("notes",TradeUtils.FilterString( TradeUtils.EncodeCyrilicString(header.notes)));
            cmdDml.Parameters.AddWithValue("payType", header.payType);
            cmdDml.Parameters.AddWithValue("autoLoad", header.autoLoad);
            cmdDml.Parameters.AddWithValue("routeId", new Guid(header.routeId));
            cmdDml.Parameters.AddWithValue("orderType", header.orderType);
            SqlDataReader reader = cmd.ExecuteReader();
            Int64 idOrder = -1;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idOrder = reader.GetInt64(0);
                }
                reader.Close();
                cmdDml.Parameters.AddWithValue("id", idOrder);
                cmdDml.CommandText = sqlUpdate;
                try
                {
                    cmdDml.ExecuteNonQuery();
                }
                catch (Exception e) { result = false; }
            }
            else
            {
                reader.Close();
                cmdDml.CommandText = sqlInsert;
                try
                {
                    cmdDml.ExecuteNonQuery();
                }
                catch (Exception e) { result = false; }
            }
            cmdDml.Parameters.Clear();
            const string sqlDelete = @"delete from [dbo].[orderDetail] where [orderUUID] = @orderUUID";

            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = sqlDelete;
            cmd1.Parameters.AddWithValue("orderUUID", new Guid(header.orderUUID));
            cmd1.ExecuteNonQuery();
            cmd1.Parameters.Clear();

            con.Close();
            return result;
        }

        public static bool SaveDetailOrder(OrderDetail detail)
        {
            bool result = true;
            #region Sql Statement
            const string sqlDelete = @"delete from [dbo].[orderDetail] where [orderUUID] = @orderUUID and skuId=@skuId";
            const string sqlInsert = @"INSERT INTO [dbo].[orderDetail]
                                               ([orderUUID]
                                               ,[skuId]
                                               ,[qty1]
                                               ,[qty2], priceId, finalDate)
                                         VALUES
                                               (@orderUUID
                                               ,@skuId
                                               ,@qty1
                                               ,@qty2, @priceId, convert(datetime,@finalDate,101))";
            #endregion
            SqlConnection con = getConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlDelete;
            cmd.Parameters.AddWithValue("orderUUID", new Guid(detail.orderUUID));
            cmd.Parameters.AddWithValue("skuId", new Guid(detail.skuId));
            cmd.ExecuteNonQuery();

            SqlCommand cmdDml = con.CreateCommand();
            cmdDml.CommandType = CommandType.Text;
            cmdDml.CommandText = sqlInsert;
            cmdDml.Parameters.AddWithValue("orderUUID", new Guid(detail.orderUUID));
            cmdDml.Parameters.AddWithValue("skuId", new Guid(detail.skuId));
            cmdDml.Parameters.AddWithValue("qty1", detail.qty1);
            cmdDml.Parameters.AddWithValue("qty2", detail.qty2);
            cmdDml.Parameters.AddWithValue("priceId", new Guid(detail.priceType));
            cmdDml.Parameters.AddWithValue("finalDate", detail.finalDate);
            cmdDml.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public static void MarcOrder(Guid orderUUID)
        {
            const string sqlUpdate = @"update orderHeader set _send=0, _send2 =0  where [orderUUID]=@orderUUID";
            SqlConnection con = getConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlUpdate;
            cmd.Parameters.AddWithValue("orderUUID", orderUUID);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void SavePay(ClaimedPays pay)
        {
            const string sqlDelete = "delete from [ClaimedPays] where transactionId = @transactionId and payDate = convert(datetime,@payDate,101)";
            const string sqlInsert = @"INSERT INTO [dbo].[ClaimedPays]
                                               ([routeId]
                                               ,[payDate]
                                               ,[transactionId]
                                               ,[paySum])
                                         VALUES
                                               (@routeId
                                               ,convert(datetime,@payDate,101)
                                               ,@transactionId
                                               ,@paySum)";
            SqlConnection con = getConnection();
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlDelete;
                cmd.Parameters.AddWithValue("transactionId", new Guid(pay.transactionId));
                cmd.Parameters.AddWithValue("payDate", pay.payDate);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlInsert;
                cmd.Parameters.AddWithValue("transactionId", new Guid(pay.transactionId));
                cmd.Parameters.AddWithValue("routeId", new Guid(pay.routeId));
                cmd.Parameters.AddWithValue("paySum", pay.paySum);
                cmd.Parameters.AddWithValue("payDate", pay.payDate);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            con.Close();
        }

        public static void SaveNewCustomer(NewCustomers newCustomer)
        {
            const string commandText = @"INSERT INTO [dbo].[NewCustomers]
                                               ([RegistrationDate]
                                               ,[Territory]
                                               ,[RouteId]
                                               ,[CustomerName]
                                               ,[DeliveryAddress]
                                               ,[OutletCategoty]
                                               ,[PriceType]
                                               ,[VisitDay]
                                               ,[DeliveryDay]
                                               ,[Manager1Name]
                                               ,[Manager1Phone]
                                               ,[Manager2Name]
                                               ,[Manager2Phone]
                                               ,[AdditionalInfo]
                                               ,[processed])
                                         VALUES
                                               (convert(datetime,@RegistrationDate,104)
                                               ,@Territory
                                               ,@RouteId
                                               ,@CustomerName
                                               ,@DeliveryAddress
                                               ,@OutletCategoty
                                               ,@PriceType
                                               ,@VisitDay
                                               ,@DeliveryDay
                                               ,@Manager1Name
                                               ,@Manager1Phone
                                               ,@Manager2Name
                                               ,@Manager2Phone
                                               ,@AdditionalInfo
                                               ,0)";
            SqlConnection con = getConnection();
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = commandText;
                cmd.Parameters.AddWithValue("RegistrationDate", newCustomer.registrationDate);
                cmd.Parameters.AddWithValue("Territory", new Guid(newCustomer.territory));
                cmd.Parameters.AddWithValue("RouteId", new Guid(newCustomer.routeId));
                cmd.Parameters.AddWithValue("CustomerName", TradeUtils.FilterString(TradeUtils.EncodeCyrilicString(newCustomer.CustomerName)));
                cmd.Parameters.AddWithValue("DeliveryAddress", TradeUtils.FilterString(TradeUtils.EncodeCyrilicString(newCustomer.DeliveryAddress)));
                cmd.Parameters.AddWithValue("OutletCategoty", Convert.ToInt32(newCustomer.OutletCategoty));
                cmd.Parameters.AddWithValue("PriceType", new Guid(newCustomer.PriceType));
                cmd.Parameters.AddWithValue("VisitDay", Convert.ToInt32(newCustomer.VisitDay));
                cmd.Parameters.AddWithValue("DeliveryDay", Convert.ToInt32(newCustomer.DeliveryDay));
                cmd.Parameters.AddWithValue("Manager1Name", TradeUtils.FilterString(TradeUtils.EncodeCyrilicString(newCustomer.Manager1Name)));
                cmd.Parameters.AddWithValue("Manager2Name", TradeUtils.FilterString(TradeUtils.EncodeCyrilicString(newCustomer.Manager2Name)));
                cmd.Parameters.AddWithValue("Manager1Phone", TradeUtils.FilterString(TradeUtils.EncodeCyrilicString(newCustomer.Manager1Phone)));
                cmd.Parameters.AddWithValue("Manager2Phone", TradeUtils.FilterString(TradeUtils.EncodeCyrilicString(newCustomer.Manager2Phone)));
                cmd.Parameters.AddWithValue("AdditionalInfo", TradeUtils.FilterString(TradeUtils.EncodeCyrilicString(newCustomer.AdditionalInfo)));
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(Exception e)
                {

                }
                finally
                {
                    cmd.Parameters.Clear();
                }
            }
            con.Close();
        }

        public static void SaveNoResult(NoResultData[] noresult)
        {
            const string commandText = @"
                                        INSERT INTO [dbo].[NoResultVisitReasons]
                                                   ([Date]
                                                   ,[outletid]
                                                   ,[reasonid])
                                             VALUES
                                                   (convert(datetime,@Date,101)
                                                   ,@outletid
                                                   ,@reasonid)";
            SqlConnection con = getConnection();
            foreach (var item in noresult)
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = commandText;
                    cmd.Parameters.AddWithValue("Date", item.Date);
                    cmd.Parameters.AddWithValue("outletid",new Guid(item.OutletId));
                    cmd.Parameters.AddWithValue("reasonid", item.ReasonId);
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
        }

        public static void SaveTask(ManagersTask[] tasks)
        {
            const string commandText = @"update _Document10817
                                                set _Fld10821RRef = 0x8D3BB07AA22FAF024FBB027A60ACAEAB,
	                                                _Fld10823 = @descr
                                                where _Number= @number";
            SqlConnection con = get1cConnection();
            foreach (var task in tasks)
            {
#if DEBUG
                Debug.WriteLine(task);
                Console.WriteLine(task);
#endif
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = commandText;
                    cmd.Parameters.AddWithValue("descr", TradeUtils.FilterString(TradeUtils.EncodeCyrilicString(task.ResultDescription)));
                    cmd.Parameters.AddWithValue("number", task.Number);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception) { }
                }
            }
            con.Close();

        }
    }
    
    
}