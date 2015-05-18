using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1CBranches : _1CUtManager, IResultArray<Branch>
    {
        private _1CUtilsEnterra._1CEntParameter[] paramList;
        #region _1cquery
        const string _1CQuery = "выбрать ссылка id , Наименование Description из Справочник.Организации";
        #endregion

        public Branch[] ConvertToArray(IQueryable<System.Data.DataRow> queryable = null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<Branch> result =
                     from row in queryable
                     select new Branch()
                         {
                             Id = new Guid(row["id"].ToString()),
                             Description = row["Description"].ToString()
                         };

            return result.Cast<Branch>().ToArray();
        }

        public _1CBranches(_1CUtilsEnterra._1CEntParameter[] paramList = null)
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