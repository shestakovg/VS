using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1CRouteDebtParams : _1CUtManager, IResultArray<RouteDebtParams>
    {
        private _1CUtilsEnterra._1CEntParameter[] paramList;
        private string _1CQuery = @"ВЫБРАТЬ
	                                    case when МаршрутыТорговыхПредставителей.КонтрольПросрочки then 1 
	                                    else 0 end debtControl,
	                                    МаршрутыТорговыхПредставителей.МаксимальнаяСуммаПросрочки allowOverdueSum,
                                        5 as skuQty,
                                        500 as minOrderSum
                                    ИЗ
	                                    Справочник.МаршрутыТорговыхПредставителей КАК МаршрутыТорговыхПредставителей
                                    ГДЕ
	                                    МаршрутыТорговыхПредставителей.Ссылка = &RouteID";
        public RouteDebtParams[] ConvertToArray(IQueryable<System.Data.DataRow> queryable = null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }
            IEnumerable<RouteDebtParams> result =
                    from row in queryable
                    select new RouteDebtParams()
                    {
                        debtControl = Int32.Parse(row["debtControl"].ToString()),
                        allowOverdueSum = Int32.Parse(row["allowOverdueSum"].ToString()),
                        skuQty = Int32.Parse(row["skuQty"].ToString()),
                        minOrderSum = Convert.ToDouble(row["minOrderSum"])
                    };
            return result.Cast<RouteDebtParams>().ToArray();
        }

        public _1CRouteDebtParams(string routeId)
            : base()
        {
            if (!String.IsNullOrEmpty(routeId))
            {
                var entParameter = new _1CUtilsEnterra._1CEntParameter();
                entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "МаршрутыТорговыхПредставителей", routeId, "RouteID");
                this.paramList = new _1CUtilsEnterra._1CEntParameter[1] { entParameter };
            }
            else
            {

            }
        }

        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }
    }
}