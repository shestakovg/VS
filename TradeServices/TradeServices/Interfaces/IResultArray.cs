using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using _1CUtilsEnterra;


namespace TradeServices.Interfaces
{
    public interface IResultArray<T> where T : class
    {
        T[] ConvertToArray(IQueryable<DataRow> queryable = null);
    }

    public interface I1CUtManager
    {
        IQueryable GetQueryResult(string C1QueryText, _1CEntParameter[] paramList = null);
        void Connect1C();
    }
}
