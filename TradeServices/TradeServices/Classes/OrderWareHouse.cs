using _1C.V8.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TradeServices.Classes;

namespace TradeServices.DataEntitys
{

    public enum OrdersWH
    {
        MainWareHouse, 
        ReatilWareHose
    }

    public class OrderWareHouse: Order
    {
        private bool initialized = false;
        public bool Initialized
        {
            get { return initialized; }
        }

        private SqlConnection connection;
        private OrdersWH orderWh;
        public OrderWareHouse(long orderId, SqlConnection connection, OrdersWH orderWh, V8DbConnection _1cConnection)
            : base(orderWh, _1cConnection)
        {
            this.orderid = orderId;
            this.connection = connection;
            this.orderWh = orderWh;
            this.log = new ProcessOrderLog(connection);
            loadOrderFromDB();
            loadPositionFromDb();
        }

        private void loadOrderFromDB()
        {
            const string commadText = @"SELECT 
                                      [orderUUID]
                                      ,[outletId]
                                      ,[orderDate]
                                      ,[deliveryDate]
                                      ,[orderNumber]
                                      ,[notes]
                                      ,[payType]
                                      ,[autoLoad]
                                      ,[_1CDocId1]
                                      ,[_1CDocId2]
                                  FROM [dbo].[orderHeader] with (nolock)
                                  where 	[id] = @id";
            log.WriteLog(this.orderUUID, "Получение шапки OrderWareHouse");
            SqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = commadText;
            cmd.Parameters.AddWithValue("id", this.orderid);
            string mgs;
            SqlDataReader reader =  cmd.ExecuteReader();
           
            if (reader.HasRows)
            {
                reader.Read();
                this.orderUUID = new Guid(reader["orderUUID"].ToString());
                this.outletId = new Guid(reader["outletId"].ToString());
                this.orderDate = (DateTime)reader["orderDate"];
                this.notes = reader["notes"].ToString();
                this.payType = (int) reader["payType"];
                this.autoLoad = (int)reader["autoLoad"];
                this.deliveryDate = (DateTime)reader["deliveryDate"];
                this._1CDocId = Guid.Empty;
                if (orderWh == OrdersWH.MainWareHouse)
                {
                    if (reader["_1CDocId1"] != DBNull.Value) this._1CDocId = new Guid(reader["_1CDocId1"].ToString());
                }
                else if (orderWh == OrdersWH.ReatilWareHose)
                {
                    if (reader["_1CDocId2"] != DBNull.Value) this._1CDocId = new Guid(reader["_1CDocId2"].ToString());
                }
                //this.initialized = true;
                mgs = "Шапка OrderWareHouse запонена";
            }
            else
            {
               mgs = "Шапка OrderWareHouse пуста";
            }
            reader.Close();
            reader.Dispose();
            cmd.Dispose();
            log.WriteLog(this.orderUUID, mgs);
        }

        private void loadPositionFromDb()
        {
            string msg = "";
            const string commandText = @"SELECT id
                                              ,[orderUUID]
                                              ,[skuId]
                                              ,[qty1]
                                              ,[qty2]
                                              ,[priceId]
                                          FROM [dbo].[orderDetail] with (nolock)
                                          where [orderUUID] = @orderUUID";
            log.WriteLog(this.orderUUID, "Позиции OrderWareHouse ");
            SqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = commandText;
            cmd.Parameters.AddWithValue("orderUUID", this.orderUUID);
            SqlDataReader reader =  cmd.ExecuteReader();
            
            if (reader.HasRows)
            {
                List<OrderPosition> posList = new List<OrderPosition>();
                while (reader.Read())
                {
                    int qty = ((orderWh == OrdersWH.MainWareHouse) ? Convert.ToInt32(reader["qty1"]) : Convert.ToInt32(reader["qty2"]));
                    if (qty > 0) posList.Add(new OrderPosition(new Guid(reader["skuId"].ToString()), qty,(reader["priceId"]==DBNull.Value ? Guid.NewGuid() : new Guid(reader["priceId"].ToString()))));
                }
                if (posList.Count > 0)
                {
                    this.positions = posList.ToArray();
                    this.initialized = true;
                    msg = "Позиции OrderWareHouse заполнены";
                }
                else
                {
                    msg = "Нет позиций с количеством OrderWareHouse ";
                }

            }
            else
            {
                msg ="Позиций OrderWareHouse нет";
            }

            reader.Close();
            cmd.Dispose();
            reader.Dispose();
            log.WriteLog(this.orderUUID, msg);
        }

        public void ApplyToSql()
        {
            const string cmdText1 = @"
                                        update dbo.orderHeader
                                        set _1CDocNumber1 = @DocNumber,
	                                        _1CDocId1 = @DocId, _send = 1
                                        where id = @id";
            const string cmdText2 = @"
                                        update dbo.orderHeader
                                        set _1CDocNumber2 = @DocNumber,
	                                        _1CDocId2 = @DocId, _send2 = 1
                                        where id = @id";
            log.WriteLog(this.orderUUID, "Отметим заказ как обработанный");                    
            SqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = (orderWh == OrdersWH.MainWareHouse ? cmdText1 : cmdText2);
            cmd.Parameters.AddWithValue("DocNumber", this._1CDocNumber);
            cmd.Parameters.AddWithValue("DocId", this._1CDocId);
            cmd.Parameters.AddWithValue("id", this.orderid);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            log.WriteLog(this.orderUUID, "Заказ отмечен как обработанный");
            cmd.Dispose();
        }
    }
}
