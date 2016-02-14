using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;





namespace TradeServices.Classes
{
    public class _1CRoutes : _1CUtManager, IResultArray<Route>
    {
        private _1CUtilsEnterra._1CEntParameter[] paramList;
        private const string _1CQuery = @"ВЫБРАТЬ
	                                            МаршрутыТорговыхПредставителей.Ссылка КАК Id,
	                                            МаршрутыТорговыхПредставителей.Наименование КАК Description,
	                                            МаршрутыТорговыхПредставителей.Код КАК Code,
	                                            МаршрутыТорговыхПредставителей.Сотрудник КАК EmployeeId,
	                                            МаршрутыТорговыхПредставителей.Сотрудник.Наименование КАК Employee,
	                                            ПРЕДСТАВЛЕНИЕ(МаршрутыТорговыхПредставителей.ТипМаршрута.ССылка) как RouteType,
	                                            МаршрутыТорговыхПредставителей.ТипМаршрута.Порядок как RouteTypeID	
                                            ИЗ
	                                            Справочник.МаршрутыТорговыхПредставителей КАК МаршрутыТорговыхПредставителей
                                            ГДЕ
	                                            МаршрутыТорговыхПредставителей.Организация = &branchid
	                                            И МаршрутыТорговыхПредставителей.Статус = ЗНАЧЕНИЕ(Перечисление.СтатусыМаршрута.Рабочий)
                                            УПОРЯДОЧИТЬ ПО 	МаршрутыТорговыхПредставителей.ТипМаршрута,
                                               МаршрутыТорговыхПредставителей.Наименование";
        public _1CRoutes(string branchId) : base()
        {   
             
             if (!String.IsNullOrEmpty(branchId))
             {
                 var entParameter = new _1CUtilsEnterra._1CEntParameter();
                 entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "Организации", branchId, "branchId");
                 this.paramList = new _1CUtilsEnterra._1CEntParameter[1] { entParameter };
             }
             else
             {
                 throw new Exception("branchId is empty");
             }
        }

        public Route[] ConvertToArray(IQueryable<System.Data.DataRow> queryable = null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<Route> result =
                     from row in queryable
                     select new Route()
                     {
                         Id = new Guid(row["id"].ToString()),
                         Description = row["Description"].ToString().Trim(),
                         Code = row["Code"].ToString(),
                         EmployeeId = (row["EmployeeId"] == DBNull.Value ? Guid.Empty : new Guid(row["EmployeeId"].ToString())),
                         Employee = row["Employee"].ToString().Trim(),
                         RouteType = row["RouteType"].ToString().Trim(),
                         RouteTypeID = Int32.Parse( row["RouteTypeID"].ToString())
                     };

            return result.Cast<Route>().ToArray();
        }

        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }
    }
}