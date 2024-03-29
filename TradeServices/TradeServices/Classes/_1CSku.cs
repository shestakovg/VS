﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;
namespace TradeServices.Classes
{
    public class _1CSku: _1CUtManager, IResultArray<Sku>
    {
    
         private _1CUtilsEnterra._1CEntParameter[] paramList;

        #region 1cquery
         private const string _1CQuery = @" ВЫБРАТЬ
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
	                                            case when Номенклатура.ПродажаТолькоГлавныйСклад then 1 else 0 end КАК onlyMWH
                                            ИЗ
	                                            Справочник.Номенклатура КАК Номенклатура
		                                            ЛЕВОЕ СОЕДИНЕНИЕ Справочник.УпаковкиНоменклатуры КАК УпаковкиНоменклатуры
		                                            ПО (УпаковкиНоменклатуры.Владелец = Номенклатура.Ссылка)
			                                            И (УпаковкиНоменклатуры.ЕдиницаИзмерения.Код = ""207"")
                                            ГДЕ
	                                            НЕ Номенклатура.ЭтоГруппа
	                                            И НЕ Номенклатура.ПометкаУдаления
	                                            И НЕ Номенклатура.Родитель.ПометкаУдаления
    
                                        ";
        #endregion
        public Sku[] ConvertToArray(IQueryable<System.Data.DataRow> queryable =  null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<Sku> result =
                    from row in queryable
                    select new Sku()
                    {
                        SkuId = new Guid(row["SkuId"].ToString()),
                        SkuName = row["SkuName"].ToString().Trim(), 
                        SkuParentId = (row["SkuParentId"] == System.DBNull.Value ? Guid.Empty : new Guid(row["SkuParentId"].ToString())),
                        Article = row["Article"].ToString().Trim(),
                        QtyPack = (row["QtyPack"] == System.DBNull.Value ? 0 :double.Parse(row["QtyPack"].ToString().Replace(".",","))),
                        OnlyFact = Int32.Parse(row["onlyFact"].ToString()),
                        CheckCountInBox = Int32.Parse(row["checkCountInBox"].ToString()),
                        OnlyMWH = Int32.Parse(row["onlyMWH"].ToString()),
                   
                    };
            return result.Cast<Sku>().ToArray();
        }

        public _1CSku(_1CUtilsEnterra._1CEntParameter[] paramList = null)
            : base() 
        {
            if (paramList != null)
            {
                this.paramList = paramList;
            }
        }
        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }
    }
}        