﻿using System;
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
        private const string _1CQuery = @"ВЫБРАТЬ
                                            ЦеныНоменклатурыСрезПоследних.Цена как Pric,
                                            ЦеныНоменклатурыСрезПоследних.Номенклатура как SkuId,
                                            ЦеныНоменклатурыСрезПоследних.ВидЦены как PriceId
                                        ИЗ
                                            РегистрСведений.ЦеныНоменклатуры.СрезПоследних(КОНЕЦПЕРИОДА(&Дата, ДЕНЬ), ВИдЦены = &priceId и не Номенклатура.ПометкаУдаления и  не Номенклатура.Родитель.ПометкаУдаления) КАК ЦеныНоменклатурыСрезПоследних
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

     public _1CPrice(string priceId)
            : base()
        {

           
                _1CUtilsEnterra._1CEntParameter paramDate = new _1CUtilsEnterra._1CEntParameter("Дата", DateTime.Today);
                _1CUtilsEnterra._1CEntParameter entParameter = new _1CUtilsEnterra._1CEntParameter();
                entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "ВидыЦен", priceId, "priceId");
                this.paramList = new _1CUtilsEnterra._1CEntParameter[2] { paramDate, entParameter};
           
        }
        //
         public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }

    }
}