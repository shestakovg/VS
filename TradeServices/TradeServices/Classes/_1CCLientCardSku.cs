using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1CClientCardSku : _1CUtManager, IResultArray<ClientCardSku>
    {
         private _1CUtilsEnterra._1CEntParameter[] paramList;

        #region 1cquery

         private const string _1CQuery = @"ВЫБРАТЬ ТорговаяТочка as OutletId ,Номенклатура as SkuId из
                (ВЫБРАТЬ
	                ЗаказКлиентаТовары.Ссылка.ТорговаяТочка,
	                ЗаказКлиентаТовары.Номенклатура,
	                СУММА(ЗаказКлиентаТовары.Сумма) КАК Сумма
                ИЗ
	                Документ.ЗаказКлиента.Товары КАК ЗаказКлиентаТовары
                ГДЕ
	                ЗаказКлиентаТовары.Ссылка.Проведен
	                И ЗаказКлиентаТовары.Ссылка.Дата МЕЖДУ ДОБАВИТЬКДАТЕ(&Дата, МЕСЯЦ, -3) И &Дата
	                И ЗаказКлиентаТовары.Ссылка.ТорговаяТочка в
	                (ВЫБРАТЬ
			                СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка
		                ИЗ
			                РегистрСведений.СоставМаршрутаТорговогоАгента.СрезПоследних(&Дата, Маршрут = &Маршрут) КАК СоставМаршрутаТорговогоАгентаСрезПоследних)

                СГРУППИРОВАТЬ ПО
	                ЗаказКлиентаТовары.Номенклатура , ЗаказКлиентаТовары.Ссылка.ТорговаяТочка
	                ) а
                упорядочить по ТорговаяТочка	";
        #endregion
        
        public _1CClientCardSku(string routeId)
            : base()
        {

            if (!String.IsNullOrEmpty(routeId))
             {
                 var entParameter = new _1CUtilsEnterra._1CEntParameter();
                 entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "МаршрутыТорговыхПредставителей", routeId, "Маршрут");
                 _1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("Дата", DateTime.Today);
               
               
                this.paramList = new _1CUtilsEnterra._1CEntParameter[2] { entParameter, paramDate };
             }
             else
             {
                // throw new Exception("routeId is empty");
             }
        }

        public ClientCardSku[] ConvertToArray(IQueryable<System.Data.DataRow> queryable = null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }
            IEnumerable<ClientCardSku> result =
                    from row in queryable
                    select new ClientCardSku()
                    {
                        OutletId = new Guid(row["OutletId"].ToString()),
                        SkuId = new Guid(row["SkuId"].ToString())
                    };

            return result.Cast<ClientCardSku>().ToArray();
             
        }

        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }
    }
}