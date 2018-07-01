using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1CTask : _1CUtManager, IResultArray<ManagersTask>
    {
        private _1CUtilsEnterra._1CEntParameter[] paramList;
        private const string _1CQuery = @"ВЫБРАТЬ
	                                        ЗадачаТорговогоПредставителя.Ссылка reference,
	                                        ЗадачаТорговогоПредставителя.Номер Number,
	                                        ЗадачаТорговогоПредставителя.Дата DocDate,
	                                        ЗадачаТорговогоПредставителя.МаршрутТорговогоПредставителя routeId,
	                                        ЗадачаТорговогоПредставителя.ТорговаяТочка outletId,
	                                        ЗадачаТорговогоПредставителя.ОписаниеЗадачи Description
                                        ИЗ
	                                        Документ.ЗадачаТорговогоПредставителя КАК ЗадачаТорговогоПредставителя
                                        где Дата между НАЧАЛОПЕРИОДА(&Период, День) и КОНЕЦПЕРИОДА(&Период, День)
                                        и ЗадачаТорговогоПредставителя.МаршрутТорговогоПредставителя = &RouteID
                                        и ЗадачаТорговогоПредставителя.Статус = ЗНАЧЕНИЕ(Перечисление.СтатусЗадачТорговогоПредставителя.Создана)
                                        и ЗадачаТорговогоПредставителя.Проведен";

        public _1CTask(string routeId) : base()
        {
            if (!String.IsNullOrEmpty(routeId))
            {
                var entParameter = new _1CUtilsEnterra._1CEntParameter();
                entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "МаршрутыТорговыхПредставителей", routeId, "RouteID");
                _1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("Период", DateTime.Today.AddDays(0));
                this.paramList = new _1CUtilsEnterra._1CEntParameter[2] { entParameter, paramDate };
            }
        }

        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }

        public ManagersTask[] ConvertToArray(IQueryable<DataRow> queryable = null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<ManagersTask> result =
                    from row in queryable
                    select new ManagersTask()
                    {
                        Reference = new Guid(row["reference"].ToString()),
                        Number = row["Number"].ToString(),
                        RouteId = new Guid(row["routeId"].ToString()),
                        OutletId = new Guid(row["outletId"].ToString()),
                        Description = row["Description"].ToString()
                    };
            return result.Cast<ManagersTask>().ToArray();
        }
    }
}