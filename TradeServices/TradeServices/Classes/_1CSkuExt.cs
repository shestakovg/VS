using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;
namespace TradeServices.Classes
{
    public class _1CSkuExt: _1CUtManager, IResultArray<SkuExt>
    {
    
         private _1CUtilsEnterra._1CEntParameter[] paramList;

        #region 1cquery
         private const string _1CQuery = @" 
                                             ВЫБРАТЬ  	
	                                                    ПланыПродажПредварительныеСрезПоследних.Номенклатура НоменклатураПлан
                                                    поместить Планы	
                                                    ИЗ
	                                                    РегистрСведений.ПланыПродажПредварительные.СрезПоследних(,МаршрутТП = &RouteId) КАК ПланыПродажПредварительныеСрезПоследних
                                                    Где ПланыПродажПредварительныеСрезПоследних.Период в	
	                                                    (
		                                                    ВЫБРАТЬ
			                                                    max(пп.Период)
		                                                    ИЗ
			                                                    РегистрСведений.ПланыПродажПредварительные.СрезПоследних(,МаршрутТП = &RouteId)  пп
	                                                    )
                                                    И (ПланыПродажПредварительныеСрезПоследних.КоличествоТТ 
                                        			+ ПланыПродажПредварительныеСрезПоследних.Сумма) > 0;
	
	
	
                                                    ВЫБРАТЬ
	                                                    Номенклатура.Ссылка КАК SkuId,
	                                                    ВЫБОР
		                                                    КОГДА Номенклатура.НаименованиеСокращенное = """"
			                                                    ТОГДА Номенклатура.Наименование
		                                                    ИНАЧЕ Номенклатура.НаименованиеСокращенное
	                                                    КОНЕЦ КАК SkuName,
	                                                    Номенклатура.Родитель КАК SkuParentId,
	                                                    УпаковкиНоменклатуры.Коэффициент КАК QtyPack,
	                                                    Номенклатура.Артикул КАК Article,
	                                                    ВЫБОР
		                                                    КОГДА Номенклатура.ПродажаТолькоФакт
			                                                    ТОГДА 0
		                                                    ИНАЧЕ 0
	                                                    КОНЕЦ КАК onlyFact,
	                                                    ВЫБОР
		                                                    КОГДА Номенклатура.КонтроллироватьКратностьГлСклад
			                                                    ТОГДА 0
		                                                    ИНАЧЕ 1
	                                                    КОНЕЦ КАК checkCountInBox,
	                                                    ВЫБОР
		                                                    КОГДА Номенклатура.ПродажаТолькоГлавныйСклад
			                                                    ТОГДА 1
		                                                    ИНАЧЕ 0
	                                                    КОНЕЦ КАК onlyMWH,
	                                                    выбор   когда Номенклатура.Акция тогда ""#FFA80505""
			                                                    когда Номенклатура.Топ тогда ""#FF109704""

                                                                когда п.НоменклатураПлан is not null  тогда ""#FF040B97""

                                                        Иначе       ""#FF000002""	

                                                        Конец Color,
                                                        выбор когда Номенклатура.Акция тогда 	""#FFE4AAAA""

                                                                иначе ""#bdbdbd""

                                                        Конец OutStockColor,
                                                        Номенклатура.МинимальноеКоличествоЗаказа	MinOrderQty,
                                                        Выбор когда Номенклатура.ПрайсХорека тогда 1 иначе 0 конец isHoreca
                                                    ИЗ
                                                        Справочник.Номенклатура КАК Номенклатура
                                                    ЛЕВОЕ СОЕДИНЕНИЕ Справочник.УпаковкиНоменклатуры КАК УпаковкиНоменклатуры
                                                            ПО (УпаковкиНоменклатуры.Владелец = Номенклатура.Ссылка)

                                                                И(УпаковкиНоменклатуры.ЕдиницаИзмерения.Код = ""207"")
                                                    ЛЕВОЕ СОЕДИНЕНИЕ Планы п по п.НоменклатураПлан =  Номенклатура.Ссылка.Родитель
                                                    ГДЕ

                                                        НЕ Номенклатура.ЭтоГруппа
                                                        И НЕ Номенклатура.ПометкаУдаления
                                                        И НЕ Номенклатура.Родитель.ПометкаУдаления";
        #endregion
        public SkuExt[] ConvertToArray(IQueryable<System.Data.DataRow> queryable =  null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<SkuExt> result =
                    from row in queryable
                    select new SkuExt()
                    {
                        SkuId = new Guid(row["SkuId"].ToString()),
                        SkuName = row["SkuName"].ToString().Trim(), 
                        SkuParentId = (row["SkuParentId"] == System.DBNull.Value ? Guid.Empty : new Guid(row["SkuParentId"].ToString())),
                        Article = row["Article"].ToString().Trim(),
                        QtyPack = (row["QtyPack"] == System.DBNull.Value ? 0 :double.Parse(row["QtyPack"].ToString().Replace(".",","))),
                        OnlyFact = Int32.Parse(row["onlyFact"].ToString()),
                        CheckCountInBox = Int32.Parse(row["checkCountInBox"].ToString()),
                        OnlyMWH = Int32.Parse(row["onlyMWH"].ToString()),
                        Color = row["Color"].ToString(),
                        OutStockColor = row["OutStockColor"].ToString(),
                        MinOrderQty = Convert.ToInt32(row["MinOrderQty"]),
                        IsHoreca = Convert.ToInt32(row["isHoreca"])
                    };
            return result.Cast<SkuExt>().ToArray();
        }

        public _1CSkuExt(string routeId)
            : base() 
        {
            if (!String.IsNullOrEmpty(routeId))
            {
                var entParameter = new _1CUtilsEnterra._1CEntParameter();
                entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "МаршрутыТорговыхПредставителей", routeId, "RouteID");
                this.paramList = new _1CUtilsEnterra._1CEntParameter[1] { entParameter };
            }

        }
        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }
    }
}        