using _1C.V8.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TradeServices.Classes;

namespace TradeServices.DataEntitys
{
  
    

    public class Order: IDisposable
    {
        protected ProcessOrderLog log;
        protected Guid orderUUID;
        protected long orderid;
        protected Guid outletId;
        protected DateTime orderDate;
        protected DateTime deliveryDate;
        protected int payType;
        protected int autoLoad;
        protected Guid _1CDocId = Guid.Empty;
        protected string _1CDocNumber= "без номера";
        protected string wareHouse;
        protected OrderPosition[] positions;
        protected string notes = "";

        protected V8DbConnection connection;

        private ComObject orderStructure;
        private ComObject headerStucture;
        private ComObject positionValueTable;


        public Order(OrdersWH wh, ProcessOrderLog log)
        {
            this.wareHouse = (wh == OrdersWH.MainWareHouse ? "1" : "2") ;
            this.log = log;
            

        }

        public Order(OrdersWH wh,  V8DbConnection connection)
        {
            this.wareHouse = (wh == OrdersWH.MainWareHouse ? "1" : "2");
            this.connection = connection;

        }
        public bool prepare1CStructure()
        {
            
            bool result = true;
            //this.connection = _1CConnection.CreateAndOpenConnection();
            if (this.connection == null) return false;
            this.orderStructure = (ComObject)V8.Call(this.connection, this.connection.Connection, "externalGetNewStructure");
            this.headerStucture = (ComObject)V8.Call(this.connection, this.connection.Connection, "externalGetNewStructure");
            this.positionValueTable = (ComObject)V8.Call(this.connection, this.connection.Connection, "externalGetNewOrderPosValueTable");
            prepareHeader();
            preparePosition();
            
            return result;
        }

        private void prepareHeader()
        {
            log.WriteLog(this.orderUUID, "Начало обработки шапки для 1С");
            V8.Call(this.connection, this.headerStucture, "Вставить", new object[] { "НовыйЗаказ", (this._1CDocId ==  Guid.Empty ? 1 : 0) });
            V8.Call(this.connection, this.headerStucture, "Вставить", new object[] { "Дата", ПолучитьДату1СДляДокумента(DateTime.Today) });
            V8.Call(this.connection, this.headerStucture, "Вставить", new object[] { "ДатаДоставки", ПолучитьДату1СДляДокумента(this.deliveryDate) });
            V8.Call(this.connection, this.headerStucture, "Вставить", new object[] { "ТорговаяТочка", this.outletId.ToString() });
            V8.Call(this.connection, this.headerStucture, "Вставить", new object[] { "Склад", (this.wareHouse) });
            V8.Call(this.connection, this.headerStucture, "Вставить", new object[] { "docId", (this._1CDocId.ToString()) });
            V8.Call(this.connection, this.headerStucture, "Вставить", new object[] { "Самовывоз", (this.autoLoad.ToString()) });
            V8.Call(this.connection, this.headerStucture, "Вставить", new object[] { "ТипПродажи", (this.payType.ToString()) });
            V8.Call(this.connection, this.headerStucture, "Вставить", new object[] { "notes", (this.notes.ToString()) });
            V8.Call(this.connection, this.orderStructure, "Вставить", new object[] { "СтруктураШапки", this.headerStucture });
            log.WriteLog(this.orderUUID, "Завершение обработки шапки для 1С");
        }

        private void preparePosition()
        {
            log.WriteLog(this.orderUUID, "Начало обработки таб части для 1С");
            foreach (OrderPosition pos in positions)
            {
                V8.Call(this.connection, this.connection.Connection, "externalAddPos", new object[] { this.positionValueTable , pos.SkuId.ToString(), pos.Quantity, pos.priceId.ToString()});
            }
            V8.Call(this.connection, this.orderStructure, "Вставить", new object[] { "Позиции", this.positionValueTable});
            log.WriteLog(this.orderUUID, "Завершение обработки таб части для 1С");
        }

        public bool createOrder()
        {
            bool result = false;
            try
            {
                string createRes = (string)V8.Call(this.connection, this.connection.Connection, "externalCreateOrder", new object[] { this.orderStructure });
                //if (createRes == "OK")
                if (this._1CDocId == Guid.Empty)
                {
                    this._1CDocId = new Guid (
                               (string)V8.Call(this.connection, this.connection.Connection, "externalGetRef", new object[] {this.orderStructure})
                            );
                    this._1CDocNumber =
                            (string)V8.Call(this.connection, this.connection.Connection, "externalGetDocnumber", new object[] { this.orderStructure });
                            
                    result = true;
                    log.WriteLog(this.orderUUID, "Заказ создан успешно");
                }
                else
                {
                    result = true;
                    log.WriteLog(this.orderUUID, "Заказ изменен успешно");
                }
            }
            catch (Exception e)
            {
                log.WriteLog(this.orderUUID, "Err Create: "+e.InnerException.Message);
            }
            //ПРоведем если все ок
            if (result)
            {
                try
                {
                    string s = (string)V8.Call(this.connection, this.connection.Connection, "postOrder", new object[] { this._1CDocId.ToString() });
                    log.WriteLog(this.orderUUID, "Проведен успешно");
                }
                catch (Exception e)
                {
                    log.WriteLog(this.orderUUID, "Err Post: " + e.InnerException.Message);
                }
            }
            return result;
        }
        

        public static string ПолучитьДату1СДляДокумента(DateTime dt)
        {
            string s, resS;

            resS =
                dt.Year.ToString();

            s = dt.Month.ToString();
            if (s.Length == 1) s = "0" + s;
            resS = resS + s;

            s = dt.Day.ToString();
            if (s.Length == 1) s = "0" + s;
            resS = resS + s;

            s = dt.Hour.ToString();
            if (s.Length == 1) s = "0" + s;
            resS += s;

            s = dt.Minute.ToString();
            if (s.Length == 1) s = "0" + s;
            resS += s;

            s = dt.Second.ToString();
            if (s.Length == 1) s = "0" + s;
            resS += s;
            return resS;
        }



        public void Dispose()
        {
            //if (connection != null)
            //{
            //    _1CConnection.Close1CConnection(connection);
            //    V8.ReleaseComObject(connection);
            //    connection = null;
                
            //}
            if (orderStructure != null)
            {

                V8.ReleaseComObject(orderStructure);
                orderStructure = null;
            }
            if (headerStucture != null)
            {
                V8.ReleaseComObject(headerStucture);
                headerStucture = null;
            }
            if (positionValueTable != null)
            {
                V8.ReleaseComObject(positionValueTable);
                positionValueTable = null;
            }
        }
    }
}