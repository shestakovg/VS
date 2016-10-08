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
	                                                    И ПланыПродажПредварительныеСрезПоследних.КоличествоТТ + ПланыПродажПредварительныеСрезПоследних.Сумма > 0;

                                                    выбрать ном.ссылка как номенклатурнаяГруппа   
	                                                    поместить Бренды
                                                    из Справочник.Номенклатура ном

                                                    где ном.Ссылка в иерархии (
	                                                    выбрать бп.Бренд 

	                                                    из СПравочник.БрендыКПродаже.Бренды бп
	                                                    где бп.Ссылка в (выбрать БрендКПРодаже из
		                                                    Справочник.МаршрутыТорговыхПредставителей где ссылка =&RouteID ))
                                                    и ном.ЭтоГруппа и не ном.ПометкаУдаления;
                                           
                                                    ВЫБРАТЬ
                                                        Номенклатура.Ссылка как GroupId
                                                    поместить группы    
                                                    ИЗ
                                                        Справочник.Номенклатура КАК Номенклатура
                                                    Левое соединение Справочник.МаршрутыТорговыхПредставителей мтп по мтп.ССылка = &RouteID   
                                                    левое соединение Бренды бп по бп.номенклатурнаяГруппа =   Номенклатура.Ссылка
                                                    ГДЕ
                                                        Номенклатура.ЭтоГруппа и не Номенклатура.ПометкаУдаления
                                                        и 
                                                        (бп.номенклатурнаяГруппа <>ЗНАЧЕНИЕ(Справочник.Номенклатура.ПустаяСсылка) или 
    	                                                    мтп.БрендКПродаже= ЗНАЧЕНИЕ(Справочник.БрендыКПРодаже.ПустаяССылка)
                                                        );

                                                    выбрать группы.GroupId,
		                                                    группы.GroupName,
		                                                    группы.GroupParentId,
		                                                    группы.GroupParentName,
		                                                    isnull(п.Сумма,0) Amount,
		                                                    isnull(п.КоличествоТТ,0) OutletCount
                                                    из
                                                    (ВЫБРАТЬ
                                                                    Номенклатура.Ссылка как GroupId,
                                                                    Номенклатура.Наименование как GroupName,
                                                                    Номенклатура.Родитель как GroupParentId,
                                                                    Номенклатура.Родитель.Наименование как GroupParentName
                                                                ИЗ
                                                                    Справочник.Номенклатура КАК Номенклатура
                                                                ГДЕ
                                                    Номенклатура.Ссылка в иерархии (выбрать groupid из группы)            
                                                     и Номенклатура.ЭтоГруппа
                                                    объединить
                                                    ВЫБРАТЬ
                                                                    Номенклатура.Ссылка как GroupId,
                                                                    Номенклатура.Наименование как GroupName,
                                                                    Номенклатура.Родитель как GroupParentId,
                                                                    Номенклатура.Родитель.Наименование как GroupParentName
                                                                ИЗ
                                                                    Справочник.Номенклатура КАК Номенклатура
                                                    внутреннее соединение группы по группы.groupid.Родитель =   Номенклатура.Ссылка 
                                                    ГДЕ   Номенклатура.Родитель=ЗНАЧЕНИЕ(Справочник.Номенклатура.ПустаяСсылка) и Номенклатура.ЭтоГРуппа) группы
                                                    левое соединение Планы п по п.НоменклатураПлан = группы.GroupId 	";
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
                        OutletCount = Convert.ToInt32(row["OutletCount"])
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
    
