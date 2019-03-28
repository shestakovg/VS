using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
	public class _1CPriceChanges : _1CUtManager, IResultArray<PriceChanges>
	{
		private _1CUtilsEnterra._1CEntParameter[] paramList;
		#region 1cquery
		private const string _1CQuery = @"ВЫБРАТЬ
											ИзмененныеЦеныСрезПоследних.Период Date,
											ИзмененныеЦеныСрезПоследних.Номенклатура SkuId,
											ИзмененныеЦеныСрезПоследних.ВидЦены PriceId,
											ИзмененныеЦеныСрезПоследних.СтараяЦена OldPrice,
											ИзмененныеЦеныСрезПоследних.Цена NewPrice
										ИЗ
											РегистрСведений.ИзмененныеЦены.СрезПоследних(КОНЕЦПЕРИОДА(&Дата,День), ) КАК ИзмененныеЦеныСрезПоследних
										Где ИзмененныеЦеныСрезПоследних.Период >= ДОБАВИТЬКДАТЕ(&Дата,ДЕНЬ,-3)
										и ИзмененныеЦеныСрезПоследних.СтараяЦена <> ИзмененныеЦеныСрезПоследних.Цена";
		#endregion

        public _1CPriceChanges(): base()
        {
            _1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("Дата", DateTime.Today);
            this.paramList = new _1CUtilsEnterra._1CEntParameter[] {paramDate};
        }
        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }

        public PriceChanges[] ConvertToArray(IQueryable<DataRow> queryable = null)
		{
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<PriceChanges> result =
                   from row in queryable
                   select new PriceChanges()
                   {
                       Date = TradeUtils.Convert1CDateToTextDate(row["Date"].ToString()),
                       SkuId = new Guid(row["SkuId"].ToString()),
                       PriceId = new Guid(row["PriceId"].ToString()),
                       OldPrice = Convert.ToDouble(row["OldPrice"]),
                       NewPrice = Convert.ToDouble(row["NewPrice"])
                   };

            return result.Cast<PriceChanges>().ToArray();
        }
	}
}