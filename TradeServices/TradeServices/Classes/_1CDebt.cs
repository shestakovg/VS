using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1CDebt : _1CUtManager, IResultArray<DebtData>
    {
        private _1CUtilsEnterra._1CEntParameter[] paramList;
        #region 1cquery
        private const string _1CQuery = @"ВЫБРАТЬ РАЗЛИЧНЫЕ
	                                                СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка КАК OutletId,
	                                                СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Владелец КАК CustomerId,
	                                                СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка.Владелец.Партнер КАК PartnerID
                                                ПОМЕСТИТЬ Маршрут
                                                ИЗ
	                                                РегистрСведений.СоставМаршрутаТорговогоАгента.СрезПоследних(, Маршрут = &RouteID) КАК СоставМаршрутаТорговогоАгентаСрезПоследних
		                                                ЛЕВОЕ СОЕДИНЕНИЕ РегистрСведений.ДниПосещенияТорговыхТочек КАК ДниПосещенияТорговыхТочек
		                                                ПО СоставМаршрутаТорговогоАгентаСрезПоследних.Период = ДниПосещенияТорговыхТочек.ДатаАктуальности
			                                                И СоставМаршрутаТорговогоАгентаСрезПоследних.Маршрут = ДниПосещенияТорговыхТочек.Маршрут
			                                                И СоставМаршрутаТорговогоАгентаСрезПоследних.ТорговаяТочка = ДниПосещенияТорговыхТочек.ТорговаяТочка
                                                ;

                                                ////////////////////////////////////////////////////////////////////////////////
                                                ВЫБРАТЬ РАЗРЕШЕННЫЕ
	                                                РасчетыСКлиентамиОстатки.АналитикаУчетаПоПартнерам.Партнер КАК Партнер,
	                                                марш.CustomerId КАК Контрагент,
	                                                выразить(РасчетыСКлиентамиОстатки.ЗаказКлиента как  Документ.РеализацияТоваровУслуг) как Сделка,
	                                                РасчетыСКлиентамиОстатки.ЗаказКлиента.Дата КАК ДатаСделки,
	                                                РасчетыСКлиентамиОстатки.ЗаказКлиента.Номер КАК НомерСделки,
	                                                РасчетыСКлиентамиОстатки.ЗаказКлиента.СуммаДокумента КАК СуммаСделки,
	                                                выбор когда ЕСТЬNULL(РасчетыСКлиентамиОстатки.ЗаказКлиента.ДатаПлатежа, &Период)=ДАТАВРЕМЯ(1, 1, 1) then &Период else ЕСТЬNULL(РасчетыСКлиентамиОстатки.ЗаказКлиента.ДатаПлатежа, &Период) end КАК ДатаПлатежа,
	                                                РасчетыСКлиентамиОстатки.СуммаОстаток КАК ТекущаяЗадолженность,
	                                                ВЫБОР
		                                                КОГДА НАЧАЛОПЕРИОДА(ДОБАВИТЬКДАТЕ(&Период,ДЕНЬ,1), ДЕНЬ) >= выбор когда ЕСТЬNULL(РасчетыСКлиентамиОстатки.ЗаказКлиента.ДатаПлатежа, &Период)=ДАТАВРЕМЯ(1, 1, 1) then &Период else ЕСТЬNULL(РасчетыСКлиентамиОстатки.ЗаказКлиента.ДатаПлатежа, &Период) end 
			                                                ТОГДА РасчетыСКлиентамиОстатки.СуммаОстаток
		                                                ИНАЧЕ 0
	                                                КОНЕЦ КАК ПросроченныйДолг,
	                                                РАЗНОСТЬДАТ(выбор когда ЕСТЬNULL(РасчетыСКлиентамиОстатки.ЗаказКлиента.ДатаПлатежа, &Период)=ДАТАВРЕМЯ(1, 1, 1) then &Период else ЕСТЬNULL(РасчетыСКлиентамиОстатки.ЗаказКлиента.ДатаПлатежа, &Период) end , ДОБАВИТЬКДАТЕ(&Период,ДЕНЬ,1), ДЕНЬ) КАК ДнейПросрочки,
	                                                //ВЫБОР
	                                                //	КОГДА НЕ РасчетыСКлиентамиОстатки.ЗаказКлиента ССЫЛКА Справочник.ДоговорыКонтрагентов
	                                                //			И НЕ РасчетыСКлиентамиОстатки.ЗаказКлиента = НЕОПРЕДЕЛЕНО
	                                                //		ТОГДА РасчетыСКлиентамиОстатки.ЗаказКлиента
	                                                //	ИНАЧЕ НЕОПРЕДЕЛЕНО
	                                                //КОНЕЦ КАК Сделка
	                                                ВЫБОР
		                                                //КОГДА НАЧАЛОПЕРИОДА(ДОБАВИТЬКДАТЕ(&Период,ДЕНЬ,1), ДЕНЬ) = ЕСТЬNULL(РасчетыСКлиентамиОстатки.ЗаказКлиента.ДатаПлатежа, &Период)
			                                            //    ТОГДА ""#FFFCFF05""
														//КОГДА НАЧАЛОПЕРИОДА(ДОБАВИТЬКДАТЕ(&Период,ДЕНЬ,0), ДЕНЬ) = ЕСТЬNULL(РасчетыСКлиентамиОстатки.ЗаказКлиента.ДатаПлатежа, &Период)
			                                            //    ТОГДА ""#FFFCFF05""			                
			                                            КОГДА НАЧАЛОПЕРИОДА(ДОБАВИТЬКДАТЕ(&Период,ДЕНЬ,0), ДЕНЬ) > ЕСТЬNULL(РасчетыСКлиентамиОстатки.ЗаказКлиента.ДатаПлатежа, &Период)
			                                                ТОГДА ""#ffff2d58""                                   
		                                                ИНАЧЕ ""#FF95AAFF""                                               
		                                            КОНЕЦ КАК Color
		                                            //, РасчетыСКлиентамиОстатки.ЗаказКлиента.ДатаПлатежа
                                                ИЗ
	                                                РегистрНакопления.РасчетыСКлиентами.Остатки(&Период, АналитикаУчетаПоПартнерам.Контрагент в
		                                                (выбрать CustomerId из Маршрут)
	                                                ) КАК РасчетыСКлиентамиОстатки
                                                левое соединение (выбрать различные CustomerId из Маршрут) марш по марш.CustomerId = РасчетыСКлиентамиОстатки.АналитикаУчетаПоПартнерам.Контрагент	
                                                ГДЕ
	                                                РасчетыСКлиентамиОстатки.ЗаказКлиента ССЫЛКА Документ.РеализацияТоваровУслуг
	                                                упорядочить по   РасчетыСКлиентамиОстатки.АналитикаУчетаПоПартнерам.Партнер";
        #endregion


        public _1CDebt(string routeId):base()
        {
            if (!String.IsNullOrEmpty(routeId))
             {
                 var entParameter = new _1CUtilsEnterra._1CEntParameter();
                 entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "МаршрутыТорговыхПредставителей", routeId, "RouteID");
                 _1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("Период", DateTime.Today.AddDays(0));
                 //_1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("имя_параметра", DateTime.Today);
                 this.paramList = new _1CUtilsEnterra._1CEntParameter[2] { entParameter, paramDate};
             }
        }

        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }

        public DebtData[] ConvertToArray(IQueryable<System.Data.DataRow> queryable = null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<DebtData> result =
                    from row in queryable
                    select new DebtData()
                    {
                        partnerId = new Guid(row["Партнер"].ToString()),
                        customerId = new Guid(row["Контрагент"].ToString()),
                        transactionId = new Guid(row["Сделка"].ToString()),
                        transactionNumber = row["НомерСделки"].ToString(),
                        transactionDate = TradeUtils.Convert1CDateToTextDate( row["ДатаСделки"].ToString()),
                        transactionSum = Convert.ToDouble(row["СуммаСделки"]),
                        paymentDate = TradeUtils.Convert1CDateToTextDate(row["ДатаПлатежа"].ToString()),
                        debt = Convert.ToDouble(row["ТекущаяЗадолженность"]),
                        overdueDebt = Convert.ToDouble(row["ПросроченныйДолг"]),
                        overdueDays = Convert.ToInt32(row["ДнейПросрочки"]),
                        color = row["Color"].ToString()
                    };
            return result.Cast<DebtData>().ToArray();
        }
    }
}
