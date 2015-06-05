using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;


namespace TradeServices.Classes
{
    public class _1CPrice : _1CUtManager, IResultArray<Price>
    {

        private _1CUtilsEnterra._1CEntParameter[] paramList;

        #region 1cquery
        private const string _1CQuery = @" ВЫБРАТЬ
	                                            ЦеныНоменклатурыСрезПоследних.Цена как Pric,
	                                            ЦеныНоменклатурыСрезПоследних.Номенклатура как SkuId,
	                                            ЦеныНоменклатурыСрезПоследних.ВидЦены как PriceId
                                            ИЗ
	                                            РегистрСведений.ЦеныНоменклатуры.СрезПоследних(&Дата, ) КАК ЦеныНоменклатурыСрезПоследних
                                        ";
        #endregion
     public Price[] ConvertToArray(IQueryable<System.Data.DataRow> queryable =  null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<Price> result =
                    from row in queryable
                    select new Price()
                    {
                        SkuId = new Guid(row["SkuId"].ToString()),
                        PriceId = new Guid(row["PriceId"].ToString()),
                        Pric = double.Parse(row["Pric"].ToString())
                    };
            return result.Cast<Price>().ToArray();
        }

     public _1CPrice(string routeid)
            : base()
        {

           
                _1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("Дата", DateTime.Today);
                this.paramList = new _1CUtilsEnterra._1CEntParameter[1] { paramDate};
           
        }
        //
         public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }

    }
}