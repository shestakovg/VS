using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;
namespace TradeServices.Classes
{
    public class _1CSkuGroup: _1CUtManager, IResultArray<SkuGroup>
    {
    
         private _1CUtilsEnterra._1CEntParameter[] paramList;

        #region 1cquery
//         private const string _1CQuery = @" ВЫБРАТЬ
//	                                            Номенклатура.Ссылка как GroupId,
//	                                            Номенклатура.Наименование как GroupName,
//	                                            Номенклатура.Родитель как GroupParentId,
//	                                            Номенклатура.Родитель.Наименование как GroupParentName
//                                            ИЗ
//	                                            Справочник.Номенклатура КАК Номенклатура
//                                            ГДЕ
//	                                            Номенклатура.ЭтоГруппа и не Номенклатура.ПометкаУдаления
//                                        ";

         private const string _1CQuery = @"ВЫБРАТЬ
	ПланыПродажПредварительныеСрезПоследних.Номенклатура КАК НоменклатураПлан,
	ПланыПродажПредварительныеСрезПоследних.Сумма,
	ПланыПродажПредварительныеСрезПоследних.КоличествоТТ
ПОМЕСТИТЬ Планы
ИЗ
	РегистрСведений.ПланыПродажПредварительные.СрезПоследних(, МаршрутТП = &RouteId) КАК ПланыПродажПредварительныеСрезПоследних
ГДЕ
	ПланыПродажПредварительныеСрезПоследних.Период В
			(ВЫБРАТЬ
				МАКСИМУМ(пп.Период)
			ИЗ
				РегистрСведений.ПланыПродажПредварительные.СрезПоследних(, МаршрутТП = &RouteId) КАК пп)
	И ПланыПродажПредварительныеСрезПоследних.КоличествоТТ + ПланыПродажПредварительныеСрезПоследних.Сумма > 0
;

////////////////////////////////////////////////////////////////////////////////
ВЫБРАТЬ
	ном.Ссылка КАК номенклатурнаяГруппа
ПОМЕСТИТЬ Бренды
ИЗ
	Справочник.Номенклатура КАК ном
ГДЕ
	ном.Ссылка В ИЕРАРХИИ
			(ВЫБРАТЬ
				бп.Бренд
			ИЗ
				СПравочник.БрендыКПродаже.Бренды КАК бп
			ГДЕ
				бп.Ссылка В
					(ВЫБРАТЬ
						Справочник.МаршрутыТорговыхПредставителей.БрендКПродаже
					ИЗ
						Справочник.МаршрутыТорговыхПредставителей
					ГДЕ
						Справочник.МаршрутыТорговыхПредставителей.Ссылка = &RouteID))
	И ном.ЭтоГруппа
	И НЕ ном.ПометкаУдаления
;

////////////////////////////////////////////////////////////////////////////////
ВЫБРАТЬ
	Номенклатура.Ссылка КАК GroupId
ПОМЕСТИТЬ группы
ИЗ
	Справочник.Номенклатура КАК Номенклатура
		ЛЕВОЕ СОЕДИНЕНИЕ Справочник.МаршрутыТорговыхПредставителей КАК мтп
		ПО (мтп.Ссылка = &RouteID)
		ЛЕВОЕ СОЕДИНЕНИЕ Бренды КАК бп
		ПО (бп.номенклатурнаяГруппа = Номенклатура.Ссылка)
ГДЕ
	Номенклатура.ЭтоГруппа
	И НЕ Номенклатура.ПометкаУдаления
	И (бп.номенклатурнаяГруппа <> ЗНАЧЕНИЕ(Справочник.Номенклатура.ПустаяСсылка)
			ИЛИ мтп.БрендКПродаже = ЗНАЧЕНИЕ(Справочник.БрендыКПРодаже.ПустаяССылка))
;

////////////////////////////////////////////////////////////////////////////////
ВЫБРАТЬ
	группы.GroupId,
	группы.GroupName,
	группы.GroupParentId,
	группы.GroupParentName,
	ЕСТЬNULL(п.Сумма, 0) КАК Amount,
	ЕСТЬNULL(п.КоличествоТТ, 0) КАК OutletCount,
	ВЫБОР
		КОГДА ЕСТЬNULL(п.КоличествоТТ, 0) + ЕСТЬNULL(п.Сумма, 0) > 0
			ТОГДА ""#FF5F9BDB""
		ИНАЧЕ ""#FFA29A9A""
	КОНЕЦ КАК Color,
	case when группы.GroupId.НеУчитыватьВПланшете then 1 else 0 end as DontUseAmountValidation
ИЗ
    (ВЫБРАТЬ
        Номенклатура.Ссылка КАК GroupId,
        Номенклатура.Наименование КАК GroupName,
        Номенклатура.Родитель КАК GroupParentId,
        Номенклатура.Родитель.Наименование КАК GroupParentName
    ИЗ

        Справочник.Номенклатура КАК Номенклатура
    ГДЕ

        Номенклатура.Ссылка В ИЕРАРХИИ

                (ВЫБРАТЬ
                    группы.GroupId
                ИЗ

                    группы)

        И Номенклатура.ЭтоГруппа


    ОБЪЕДИНИТЬ

    ВЫБРАТЬ

        Номенклатура.Ссылка,
        Номенклатура.Наименование,
        Номенклатура.Родитель,
        Номенклатура.Родитель.Наименование
    ИЗ

        Справочник.Номенклатура КАК Номенклатура
            ВНУТРЕННЕЕ СОЕДИНЕНИЕ группы КАК группы

            ПО (группы.GroupId.Родитель = Номенклатура.Ссылка)

    ГДЕ
        Номенклатура.Родитель = ЗНАЧЕНИЕ(Справочник.Номенклатура.ПустаяСсылка)
        И Номенклатура.ЭтоГруппа) КАК группы

        ЛЕВОЕ СОЕДИНЕНИЕ Планы КАК п
        ПО(п.НоменклатураПлан = группы.GroupId)";
        #endregion
        public SkuGroup[] ConvertToArray(IQueryable<System.Data.DataRow> queryable =  null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<SkuGroup> result =
                    from row in queryable
                    select new SkuGroup()
                    {
                        GroupId = new Guid(row["GroupId"].ToString()),
                        GroupName = row["GroupName"].ToString().Trim(), 
                        GroupParentId = (row["GroupParentId"] == System.DBNull.Value ? Guid.Empty : new Guid(row["GroupParentId"].ToString())),
                        GroupParentName = row["GroupParentName"].ToString().Trim(),
                        Amount = Convert.ToDouble(row["Amount"]),
                        OutletCount = Convert.ToInt32(row["OutletCount"]),
                        Color = row["Color"].ToString(),
                        DontUseAmountValidation = Convert.ToInt32(row["DontUseAmountValidation"])
                    };
            return result.Cast<SkuGroup>().ToArray();
        }

        public _1CSkuGroup(string routeId)
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
    
