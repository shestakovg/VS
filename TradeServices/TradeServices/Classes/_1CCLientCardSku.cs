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

         private const string _1CQuery = @"ВЫБРАТЬ
	//ЗаказКлиента.Ссылка,
	ЗаказКлиента.Номенклатура,
	ЗаказКлиента.Ссылка.ТорговаяТочка как ТорговаяТочка,
	МАКСИМУМ(Ссылка.Дата) Дата
поместить Таб	
ИЗ
	Документ.ЗаказКлиента.Товары КАК ЗаказКлиента
ГДЕ
	ЗаказКлиента.Ссылка.Проведен
	И ЗаказКлиента.Ссылка.Дата МЕЖДУ ДОБАВИТЬКДАТЕ(&Дата, МЕСЯЦ, -3) И &Дата
	И ЗаказКлиента.Ссылка.ТорговаяТочка в
	(ВЫБРАТЬ
			СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка
		ИЗ
			РегистрСведений.СоставМаршрутаТорговогоАгента.СрезПоследних(&Дата, Маршрут = &Маршрут) КАК СоставМаршрутаТорговогоАгентаСрезПоследних)
СГРУППИРОВАТЬ ПО
	//ЗаказКлиента.Ссылка,
	ЗаказКлиента.Номенклатура, ЗаказКлиента.Ссылка.ТорговаяТочка ;
//упорядочить по ЗаказКлиента.Ссылка.ТорговаяТочка, 	ЗаказКлиента.Номенклатура

выбрать т.ТорговаяТочка OutletId,т.Номенклатура SkuId,НАЧАЛОПЕРИОДА(т.Дата, ДЕНЬ) LastDate,
		Товары.КоличествоУпаковок as Qty  из Таб т
внутреннее соединение    Документ.ЗаказКлиента.Товары Товары
	по т.ТорговаяТочка =  Товары.Ссылка.ТорговаяТочка и т.Дата = Товары.Ссылка.Дата
	 и т.Номенклатура =    Товары.Номенклатура
//упорядочить по  т.ТорговаяТочка, т.Номенклатура";
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
                        SkuId = new Guid(row["SkuId"].ToString()),
                        LastDate = TradeUtils.Convert1CDateToTextDate(row["LastDate"].ToString()),
                        Qty = Convert.ToInt32(row["Qty"])
                    };

            return result.Cast<ClientCardSku>().ToArray();
             
        }

        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }
    }
}