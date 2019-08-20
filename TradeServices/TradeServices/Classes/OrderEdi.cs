using _1C.V8.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Edi.Dto;

namespace TradeServices.Classes
{
    public class ProcessOrderLogMock : ProcessOrderLog
    {
        

        public override void WriteLog(Guid orderUUD, string description)
        {
            Debug.WriteLine(description);
        }
    }

    public class OrderEdi: Order
    {
        public EdinLib.Dto.OrderModel.EdinOrder edinOrder { get; private set; }

        public OrderEdi(EdinLib.Dto.OrderModel.EdinOrder edinOrder) : base(OrdersWH.MainWareHouse, _1CConnection.CreateAndOpenConnection())
        {
            this.log = new ProcessOrderLogMock();
            this.edinOrder = edinOrder;
            this.notes = $"Заказ EDI {this.edinOrder.Number}";
            this.deliveryDate = DateTime.ParseExact(this.edinOrder.DeliveryDate, "yyyy-MM-dd", null);
            this.payType = 0;
            this.autoLoad = 0;
            this.wareHouse = "1";
            this.ediOrderNum = this.edinOrder.Number;
        }

        public List<EdiDtoProcessMessage> GetAllCheckingAndCreate()
        {
            List<EdiDtoProcessMessage> messages = new List<EdiDtoProcessMessage>();
            using (_1CUtManager utManager = new _1CUtManager())
            {
                this.outletId = this.getOutletId(utManager);
                if (this.outletId == Guid.Empty)
                {
                    messages.Add(new EdiDtoProcessMessage()
                                    {
                                        Error = 1,
                                        Text = $"Не найдена торговая точка с GLN {edinOrder.Head.DeliveryPlace} для контрагента с GLN {edinOrder.Head.Buyer}"
                                    }
                    );
                    return messages;
                }

                var priceId = this.getPriceId(utManager, this.outletId);
                if (priceId == Guid.Empty)
                {
                    messages.Add(new EdiDtoProcessMessage()
                    {
                        Error = 1,
                        Text = $"Не найдено соглашение"
                    }
                    );
                }

                List<OrderPosition> positions1c = new List<OrderPosition>();
                foreach (var position in edinOrder.Head.Positions)
                {
                    var skuId = this.getSkuId(utManager, position.Product);
                    if (skuId == Guid.Empty)
                    {
                        messages.Add(new EdiDtoProcessMessage()
                                    {
                                        Error = 1,
                                        Text = $"Не найдена номенклатура {position.Characteristic.Description} штрих-код  {position.Product}"
                                    }
                        );
                    }
                    else
                    {
                        positions1c.Add( new OrderPosition(skuId, Convert.ToInt32(position.OrderQuantity), priceId));
                        
                    }
                }
                positions = positions1c.ToArray();
            }

            //if (!messages.Any(x => x.Error > 0))
            //{
            //    messages.Add(new EdiDtoProcessMessage()
            //        {
            //            Error = 0,
            //            Text = $"Passed"
            //        }
            //    );
            //}

            if (this.prepare1CStructure())
            {
                if (this.createOrder())
                {
                    messages.Add(new EdiDtoProcessMessage()
                        {
                            Error = 0,
                            Text = $"Создан заказ №{this._1CDocNumber}",
                            DocumentId= this._1CDocId
                        }
                    );
                }
                else
                {
                    messages.Add(new EdiDtoProcessMessage()
                        {
                            Error = 1,
                            Text = "Не удалось создать заказ"
                        }
                    );
                }
            }
            else
            {
                messages.Add(new EdiDtoProcessMessage()
                    {
                        Error = 1,
                        Text = $"Не удалось подготовить заказ"
                    }
                );
            }

            return  messages;
        }

        private Guid getOutletId(_1CUtManager utManager)
        {
            var result = (utManager.GetQueryResult("ВЫБРАТЬ ТорговыеТочкиКонтрагентов.Ссылка as outletId, ТорговыеТочкиКонтрагентов.Наименование Name ИЗ "+
	                                        "Справочник.ТорговыеТочкиКонтрагентов КАК ТорговыеТочкиКонтрагентов "+
                                        $" ГДЕ ТорговыеТочкиКонтрагентов.GLN = \"{edinOrder.Head.DeliveryPlace}\" и ТорговыеТочкиКонтрагентов.Владелец.GLN = \"{edinOrder.Head.Buyer}\"") as IQueryable<DataRow>)
                                        .FirstOrDefault();

            if (result!= null)
            {
               return new Guid(result["outletId"].ToString());
            }
            else
            {
                return Guid.Empty;
            }
        }

        private  Guid getSkuId(_1CUtManager utManager, string barCode)
        {
            var result = (utManager.GetQueryResult("ВЫБРАТЬ	ШтрихкодыНоменклатуры.Номенклатура skuId, ШтрихкодыНоменклатуры.Номенклатура.Наименование Name ИЗ "+
                                                   " РегистрСведений.ШтрихкодыНоменклатуры КАК ШтрихкодыНоменклатуры "+
                                                   $" ГДЕ ШтрихкодыНоменклатуры.Штрихкод = \"{barCode}\" и не ШтрихкодыНоменклатуры.Номенклатура.ПометкаУдаления") as IQueryable<DataRow>)
                                        .FirstOrDefault();

            if (result != null)
            {
                return new Guid(result["skuId"].ToString());
            }
            else
            {
                return Guid.Empty;
            }
        }

        private Guid getPriceId(_1CUtManager utManager, Guid outletId)
        {
            string _1cquery = @"выбрать ВидЦен как PriceId
                   ИЗ    Справочник.СоглашенияСКлиентами
                  где Ссылка в
                (ВЫБРАТЬ
                    max(СоглашенияСКлиентами.ссылка) соглашение
                ИЗ
                    Справочник.СоглашенияСКлиентами КАК СоглашенияСКлиентами
                внутреннее соединение Справочник.ТорговыеТочкиКонтрагентов  тт по тт.Владелец =  СоглашенияСКлиентами.КОнтрагент    
                ГДЕ
                  &ТекДата между СоглашенияСКлиентами.ДатаНачалаДействия и СоглашенияСКлиентами.ДатаОкончанияДействия   
                  и   СоглашенияСКлиентами.Статус = Значение (перечисление.СтатусыСоглашенийСКлиентами.действует)
                  и тт.Ссылка = &ТорговаяТочка)
                ";
            var entParameter = new _1CUtilsEnterra._1CEntParameter();
            entParameter.AddCatalogReferenceParameterByID(utManager.ConnectionUt, "ТорговыеТочкиКонтрагентов", outletId.ToString(), "ТорговаяТочка");
            _1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("ТекДата", DateTime.Today);
            var paramList = new _1CUtilsEnterra._1CEntParameter[2] { entParameter, paramDate };
            var result = (utManager.GetQueryResult(_1cquery, paramList) as IQueryable<DataRow>).FirstOrDefault();
            if (result != null)
            {
                return new Guid(result["PriceId"].ToString());
            }
            else
            {
                return Guid.Empty;
            }
        }
    }

    
}