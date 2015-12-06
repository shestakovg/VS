using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1CSkuFact: _1CUtManager, IResultArray<SkuFact>
    {
        private _1CUtilsEnterra._1CEntParameter[] paramList;
        #region 1cquery
         private const string _1CQuery = @"ВЫБРАТЬ
	                                            НоменклатураОтгрузкаПоФакту.Ссылка SkuId,
	                                            НоменклатураОтгрузкаПоФакту.ВидЦены PriceId
                                            ИЗ
	                                            Справочник.Номенклатура.ОтгрузкаПоФакту КАК НоменклатураОтгрузкаПоФакту
                                            ГДЕ
	                                            НоменклатураОтгрузкаПоФакту.Использовать
	                                            и не НоменклатураОтгрузкаПоФакту.Ссылка.ПометкаУдаления
                                        ";
        #endregion
        public SkuFact[] ConvertToArray(IQueryable<System.Data.DataRow> queryable =  null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<SkuFact> result =
                    from row in queryable
                    select new SkuFact()
                    {
                        SkuId = new Guid(row["SkuId"].ToString()),
                        PriceId = new Guid(row["PriceId"].ToString())
                   
                    };
            return result.Cast<SkuFact>().ToArray();
        }

        public _1CSkuFact(_1CUtilsEnterra._1CEntParameter[] paramList = null)
            : base() 
        {
            if (paramList != null)
            {
                this.paramList = paramList;
            }
        }
        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery);
        }
    }
}