using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1CSpecification : _1CUtManager, IResultArray<Specification>
    {
        private _1CUtilsEnterra._1CEntParameter[] paramList;
        private const string _1CQuery = @"ВЫБРАТЬ
	                                        СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка
                                        ПОМЕСТИТЬ Маршрут
                                        ИЗ
	                                        РегистрСведений.СоставМаршрутаТорговогоАгента.СрезПоследних КАК СоставМаршрутаТорговогоАгентаСрезПоследних
                                        ГДЕ
	                                        СоставМаршрутаТорговогоАгентаСрезПоследних.Маршрут = &Маршрут
                                        ;

                                        ////////////////////////////////////////////////////////////////////////////////
                                        ВЫБРАТЬ
	                                        СпецификацияНоменклатурыСрезПоследних.Номенклатура SkuId,
	                                        СпецификацияНоменклатурыСрезПоследних.ТорговаяТочка OutletId
                                        ИЗ
	                                        РегистрСведений.СпецификацияНоменклатуры.СрезПоследних КАК СпецификацияНоменклатурыСрезПоследних
                                        внутреннее  соединение Маршрут	м по м.ТорговаяТочка = СпецификацияНоменклатурыСрезПоследних.ТорговаяТочка";

        public _1CSpecification(string routeId)
        {
            if (!String.IsNullOrEmpty(routeId))
            {
                var entParameter = new _1CUtilsEnterra._1CEntParameter();
                entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "МаршрутыТорговыхПредставителей", routeId, "Маршрут");
                this.paramList = new _1CUtilsEnterra._1CEntParameter[1] { entParameter };
            }
            else
            {
                throw new Exception("routeId is empty");
            }
        }
        
        public Specification[] ConvertToArray(IQueryable<System.Data.DataRow> queryable = null)
        {
           if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

           IEnumerable<Specification> result =
                from row in queryable
                select new Specification()
                {
                    OutletId = new Guid(row["OutletId"].ToString()),
                    SkuId = new Guid(row["SkuId"].ToString())
                };
            return result.Cast<Specification>().ToArray();
        }

        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }
    }
}