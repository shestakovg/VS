using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;


namespace TradeServices.Classes
{
    public class _1CContract : _1CUtManager, IResultArray<Contract>
    {
    
         private _1CUtilsEnterra._1CEntParameter[] paramList;

        #region 1cquery
         private const string _1CQuery = @" ВЫБРАТЬ
	                                        max(СоглашенияСКлиентами.ссылка) соглашение ,
	                                        СоглашенияСКлиентами.Партнер
                                        ПОМЕСТИТЬ сог
                                        ИЗ
	                                        Справочник.СоглашенияСКлиентами КАК СоглашенияСКлиентами
                                        ГДЕ
                                          &ТекДата между СоглашенияСКлиентами.ДатаНачалаДействия и СоглашенияСКлиентами.ДатаОкончанияДействия   
                                          и   СоглашенияСКлиентами.Статус = Значение (перечисление.СтатусыСоглашенийСКлиентами.действует)

                                        СГРУППИРОВАТЬ ПО
	                                        СоглашенияСКлиентами.Партнер
                                        ;
                                        ВЫБРАТЬ
	                                        СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Владелец как CustomerId,
	                                        СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Владелец.Наименование как  CustomerName,
	                                        СоглашенияСКлиентами.ВидЦен как PriceId,
	                                        СоглашенияСКлиентами.ВидЦен.Наименование как PriceName,
	                                        СоглашенияСКлиентами.ГрафикОплаты.Наименование   как Reprieve,
	                                        СоглашенияСКлиентами.ДопустимаяСуммаЗадолженности как LimitSum 
                                        ИЗ
	                                        РегистрСведений.СоставМаршрутаТорговогоАгента.СрезПоследних(, Маршрут = &RouteID) КАК СоставМаршрутаТорговогоАгентаСрезПоследних
		                                        ВНУТРЕННЕЕ СОЕДИНЕНИЕ сог КАК сог
		                                            ПО СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Владелец.Партнер = сог.Партнер
                                                левое соединение  Справочник.СоглашенияСКлиентами как  СоглашенияСКлиентами 
                                                    ПО сог.соглашение = СоглашенияСКлиентами.ссылка 
   
                                        ";
        #endregion
        public Contract[] ConvertToArray(IQueryable<System.Data.DataRow> queryable =  null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<Contract> result =
                    from row in queryable
                    select new Contract()
                    {
                        CustomerId = new Guid(row["CustomerId"].ToString()),
                        CustomerName = row["CustomerName"].ToString(),
                        PriceId = new Guid(row["PriceId"].ToString()),
                        PriceName = row["PriceName"].ToString(),
                        LimitSum = Int32.Parse(row["LimitSum"].ToString()),
                        Reprieve =  row["Reprieve"].ToString()    
                    };
            return result.Cast<Contract>().ToArray();
        }
        public _1CContract(string routeId)
            : base()
        {

            if (!String.IsNullOrEmpty(routeId))
             {
                 var entParameter = new _1CUtilsEnterra._1CEntParameter();
                 entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "МаршрутыТорговыхПредставителей", routeId, "RouteID");
                 _1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("ТекДата", DateTime.Today);
                this.paramList = new _1CUtilsEnterra._1CEntParameter[2] { entParameter,paramDate};
             }
            else
                {
                    // throw new Exception("routeId is empty");
                }
        }

         public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }

    }
}