using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1CSalesFact : _1CUtManager, IResultArray<SalesFact>
    {
        private _1CUtilsEnterra._1CEntParameter[] paramList;
        #region 1cquery

        private const string _1CQuery = @"ВЫБРАТЬ РАЗРЕШЕННЫЕ
	                                        ВыручкаИСебестоимостьПродажОбороты.АналитикаУчетаНоменклатуры.Номенклатура.Родитель КАК GroupId ,
	                                        СУММА(ВыручкаИСебестоимостьПродажОбороты.СуммаВыручкиОборот) КАК FactAmount,
	                                        КОЛИЧЕСТВО(РАЗЛИЧНЫЕ ВыручкаИСебестоимостьПродажОбороты.Регистратор.ТорговаяТочка) КАК FactOutletCount
	                                        //СоставМаршрутаТорговогоАгентаСрезПоследних.Маршрут
                                        //ПОМЕСТИТЬ ФактТекущий
                                        ИЗ
	                                        РегистрНакопления.ВыручкаИСебестоимостьПродаж.Обороты(НАЧАЛОПЕРИОДА(&Period, МЕСЯЦ), КОНЕЦПЕРИОДА(&Period, МЕСЯЦ), Регистратор, ) КАК ВыручкаИСебестоимостьПродажОбороты
		                                        ЛЕВОЕ СОЕДИНЕНИЕ РегистрСведений.СоставМаршрутаТорговогоАгента.СрезПоследних(КОНЕЦПЕРИОДА(&Period, МЕСЯЦ), ТипМаршрута = ЗНАЧЕНИЕ(Перечисление.ТипыМаршрутовДистрибуции.ТорговыйПредставитель)) КАК СоставМаршрутаТорговогоАгентаСрезПоследних
		                                        ПО ВыручкаИСебестоимостьПродажОбороты.ЗаказКлиента.ТорговаяТочка = СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка
                                        ГДЕ
	                                        СоставМаршрутаТорговогоАгентаСрезПоследних.Маршрут В ИЕРАРХИИ(&RouteId)

                                        СГРУППИРОВАТЬ ПО
	                                        ВыручкаИСебестоимостьПродажОбороты.АналитикаУчетаНоменклатуры.Номенклатура.Родитель,
	                                        СоставМаршрутаТорговогоАгентаСрезПоследних.Маршрут";
        #endregion
        public SalesFact[] ConvertToArray(IQueryable<DataRow> queryable = null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<SalesFact> result =
                    from row in queryable
                    select new SalesFact()
                    {
                        GroupId = new Guid(row["GroupId"].ToString()),
                        FactAmount = Convert.ToDouble(row["FactAmount"]),
                        FactOutletCount = Convert.ToInt32(row["FactOutletCount"])
                    };
            return result.Cast<SalesFact>().ToArray();
        }

        public _1CSalesFact(string routeId):base()
        {
            if (!String.IsNullOrEmpty(routeId))
            {
                var entParameter = new _1CUtilsEnterra._1CEntParameter();
                entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "МаршрутыТорговыхПредставителей", routeId, "RouteID");
                _1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("Period", DateTime.Today);
                paramList = new _1CUtilsEnterra._1CEntParameter[] { entParameter, paramDate };
            }
        }
        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }
    }
}