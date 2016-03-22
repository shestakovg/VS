﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1CRouteSet: _1CUtManager, IResultArray<RouteSet>
    {
        private _1CUtilsEnterra._1CEntParameter[] paramList;

        #region 1cquery
        private const string _1CQuery = @"
                                            ВЫБРАТЬ различные
	                                            СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка КАК OutletId,
	                                            СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Наименование КАК OutletName,
	                                            ПРЕДСТАВЛЕНИЕ(ДниПосещенияТорговыхТочек.ДеньВизита) КАК VisitDay,
	                                            ДниПосещенияТорговыхТочек.ДеньВизита.Порядок КАК VisitDayId,
	                                            ДниПосещенияТорговыхТочек.ПорядокПосещения КАК VisitOrder,
	                                            СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Владелец КАК CustomerId,
	                                            СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Владелец.Наименование КАК CustomerName,
	                                            СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Владелец.Партнер КАК PartnerID,
	                                            СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Владелец.Партнер.Наименование КАК PartnerName,
                                                СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Адрес как Address,
                                                case when ДниПосещенияТорговыхТочек.ДеньВизита.Порядок = &DayOfWeek then 1 else 0 end IsRoute
                                            ИЗ
	                                            РегистрСведений.СоставМаршрутаТорговогоАгента.СрезПоследних() КАК СоставМаршрутаТорговогоАгентаСрезПоследних
		                                            ЛЕВОЕ СОЕДИНЕНИЕ РегистрСведений.ДниПосещенияТорговыхТочек КАК ДниПосещенияТорговыхТочек
		                                            ПО СоставМаршрутаТорговогоАгентаСрезПоследних.Период = ДниПосещенияТорговыхТочек.ДатаАктуальности
			                                            И СоставМаршрутаТорговогоАгентаСрезПоследних.Маршрут = ДниПосещенияТорговыхТочек.Маршрут
			                                            И СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка = ДниПосещенияТорговыхТочек.ТорговаяТочка
                                                где ДниПосещенияТорговыхТочек.ДеньВизита is not null
                                                и СоставМаршрутаТорговогоАгентаСрезПоследних.Маршрут = &RouteID
                                                //и ДниПосещенияТорговыхТочек.ДеньВизита.Порядок = &DayOfWeek
                                            ";
        #endregion
        public RouteSet[] ConvertToArray(IQueryable<System.Data.DataRow> queryable =  null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<RouteSet> result =
                    from row in queryable
                    select new RouteSet()
                    {
                        OutletId = new Guid(row["OutletId"].ToString()),
                        OutletName = row["OutletName"].ToString(),
                        VisitDay = row["VisitDay"].ToString(),
                        VisitDayId = Int32.Parse(row["VisitDayId"].ToString()),
                        VisitOrder = Int32.Parse(row["VisitOrder"].ToString()),
                        CustomerId = new Guid(row["CustomerId"].ToString()),
                        CustomerName = row["CustomerName"].ToString(),
                        PartnerId = new Guid(row["PartnerID"].ToString()),
                        PartnerName = row["PartnerName"].ToString().Trim(),
                        address = row["address"].ToString().Trim(),
                        IsRoute = Int32.Parse(row["IsRoute"].ToString())
                    };
            
            return result.Cast<RouteSet>().ToArray();
        }

        public _1CRouteSet(string routeId)
            : base()
        {

            if (!String.IsNullOrEmpty(routeId))
             {
                 var entParameter = new _1CUtilsEnterra._1CEntParameter();
                 entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "МаршрутыТорговыхПредставителей", routeId, "RouteID");
                 //_1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("имя_параметра", DateTime.Today);
                int dayOfWeek = (int) DateTime.Today.DayOfWeek;
                 dayOfWeek = (dayOfWeek == 0 ? 6 : dayOfWeek - 1); 
                _1CUtilsEnterra._1CEntParameter paramDayOfWeek = new _1CUtilsEnterra._1CEntParameter("DayOfWeek",dayOfWeek);

                this.paramList = new _1CUtilsEnterra._1CEntParameter[2] { entParameter, paramDayOfWeek };
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