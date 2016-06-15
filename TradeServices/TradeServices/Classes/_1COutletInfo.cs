using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1COutletInfo : _1CUtManager, IResultArray<OutletInfo>
    {
        private _1CUtilsEnterra._1CEntParameter[] paramList;

        #region 1cquery

        private const string _1CQuery = @"ВЫБРАТЬ
	СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка  as OutletId,
	ISNULL(ПРЕДСТАВЛЕНИЕ(СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Категория.Ссылка),""Не указано"") как Category,
	СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.ЛПР1 as Manager1,
	СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.ЛПР2 as  Manager2,
	СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.ТелефонЛПР1 as Phone1,
	СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.ТелефонЛПР2  as Phone2,
	ISNULL(ПРЕДСТАВЛЕНИЕ(СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.ДеньДоставки.Ссылка),""Не указано"")как DeliveryDay,
	СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.ВремяРаботыЛПР as ManagerTime,
	СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.ВремяПриемаТовара as ReciveTime,
	СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Владелец.КонтактноеЛицо as ContactPerson
ИЗ
	РегистрСведений.СоставМаршрутаТорговогоАгента.СрезПоследних(, Маршрут = &RouteID) КАК СоставМаршрутаТорговогоАгентаСрезПоследних";
        #endregion

        public OutletInfo[] ConvertToArray(IQueryable<System.Data.DataRow> queryable = null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<OutletInfo> result =
                    from row in queryable
                    select new OutletInfo()
                    {
                        OutletId = new Guid(row["OutletId"].ToString()),
                        Category = row["Category"].ToString(),
                        Manager1 = row["Manager1"].ToString(),
                        Manager2 = row["Manager2"].ToString(),
                        Phone1 = row["Phone1"].ToString(),
                        Phone2 = row["Phone2"].ToString(),
                        DeliveryDay = row["DeliveryDay"].ToString(),
                        ManagerTime = row["ManagerTime"].ToString(),
                        ReciveTime = row["ReciveTime"].ToString(),
                        ContactPerson = row["ContactPerson"].ToString()
                    };

            return result.Cast<OutletInfo>().ToArray();
        }

        public _1COutletInfo(string routeId)
            : base()
        {
            if (!String.IsNullOrEmpty(routeId))
            {
                var entParameter = new _1CUtilsEnterra._1CEntParameter();
                entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "МаршрутыТорговыхПредставителей", routeId, "RouteID");
                //_1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("имя_параметра", DateTime.Today);
                this.paramList = new _1CUtilsEnterra._1CEntParameter[1] { entParameter };
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