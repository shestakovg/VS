using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TradeServices.DataEntitys
{

    public enum OrdersWareHouse
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
        private OrdersWareHouse orderWh;
        public OrderWareHouse(OrdersWareHouse orderWh)
        {
            this.orderid = orderid;
            this.connection = connection;
        }

        private void loadOrderFromDB()
        {
            const string commadText = @"SELECT 
                                      [orderUUID]
                                      ,[outletId]
                                      ,[orderDate]
                                      ,[orderNumber]
                                      ,[notes]
                                      ,[payType]
                                      ,[autoLoad]
                                      ,[_1CDocId1]
                                      ,[_1CDocId2]
                                  FROM [dbo].[orderHeader]
                                  where 	[id] = @id";
            SqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = commadText;
            cmd.Parameters.AddWithValue("id", this.orderid);
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
                this._1CDocId = Guid.Empty;
                if (orderWh == OrdersWareHouse.MainWareHouse)
                {
                    if (reader["_1CDocId1"] != DBNull.Value) this._1CDocId = new Guid(reader["_1CDocId1"].ToString());
                }
                else if (orderWh == OrdersWareHouse.ReatilWareHose)
                {
                    if (reader["_1CDocId2"] != DBNull.Value) this._1CDocId = new Guid(reader["_1CDocId2"].ToString());
                }
                //this.initialized = true;
            }
            reader.Close();
        }

        private void loadPositionFromDb()
        {
            const string commandText = @"SELECT id
                                              ,[orderUUID]
                                              ,[skuId]
                                              ,[qty1]
                                              ,[qty2]
                                          FROM [dbo].[orderDetail]
                                          where [orderUUID] = @orderUUID";
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
                    int qty = ( (orderWh == OrdersWareHouse.MainWareHouse) ?  Convert.ToInt32(reader["qty1"]) : Convert.ToInt32(reader["qty2"]));
                    if (qty>0)        posList.Add( new OrderPosition(new Guid(reader["skuId"].ToString()), qty));
                }
                if (posList.Count > 0)
                {
                    this.positions = posList.ToArray();
                    this.initialized = true;
                }
            }

            reader.Close();
        }
    }
}