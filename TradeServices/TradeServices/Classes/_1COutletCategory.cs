﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1COutletCategory : _1CUtManager, IResultArray<Enum1c>
    {
        #region _1CQuery
        private const string _1CQuery = @"ВЫБРАТЬ
	                                    ПРЕДСТАВЛЕНИЕ(КатегорииТорговыхТочек.Ссылка) Name,
	                                    КатегорииТорговыхТочек.Порядок     Order_
                                    ИЗ
	                                    Перечисление.КатегорииТорговыхТочек КАК КатегорииТорговыхТочек";
        #endregion
        public Enum1c[] ConvertToArray(IQueryable<DataRow> queryable = null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<Enum1c> result =
                    from row in queryable
                    select new Enum1c
                    {
                        //categoryRef = new Guid(row["OutletCategory"].ToString()),
                        Name = row["Name"].ToString(),
                        Order = Convert.ToInt32(row["Order_"])
                    };
            return result.Cast<Enum1c>().ToArray();
        }

        public _1COutletCategory(): base()
        {

        }

        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery);
        }
    }


}