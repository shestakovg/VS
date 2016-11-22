using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1CDeliveryArea : _1CUtManager, IResultArray<DeliveryArea>
    {
        #region _1CQuery
        private const string _1CQuery = @"ВЫБРАТЬ
	                                            РайоныДоставки.Ссылка idRef,
	                                            РайоныДоставки.Наименование  Description
                                            ИЗ
	                                            Справочник.РайоныДоставки КАК РайоныДоставки
                                            ГДЕ
	                                            НЕ РайоныДоставки.ПометкаУдаления 
                                            упорядочить по 	 Наименование";
        #endregion
        public DeliveryArea[] ConvertToArray(IQueryable<DataRow> queryable = null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<DeliveryArea> result =
                    from row in queryable
                    select new DeliveryArea
                    {
                        //categoryRef = new Guid(row["OutletCategory"].ToString()),
                        Description = row["Description"].ToString().Trim(),
                        idRef = new Guid(row["idRef"].ToString())
                    };
            return result.Cast<DeliveryArea>().ToArray();
        }

        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery);
        }
    }
}