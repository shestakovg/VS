using _1C8Trade_Srv.ClassesUt;
using _1CUtilsEnterra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.Interfaces;


namespace TradeServices.Classes
{
    public class _1CUtManager : I1CUtManager, IDisposable
    {
        private _1CEntClass connectionUt = null;

        public _1CEntClass ConnectionUt
        {
            get { return connectionUt; }
        }
        public void Connect1C()
        {
            if (this.connectionUt == null)
            {
                this.connectionUt = ConnectionManager.GetConnection();
                
            }

            if (this.connectionUt == null)
            {
                throw new Invalid1CConnect("Не удалось подключиться базе УТ 11");
            }
        }

        //public void Dispose()
        //{
        //    if (this.connectionUt != null)
        //    {
        //        this.connectionUt = null;
        //        GC.Collect();
        //    }
        //}

        public _1CUtManager()
        {
            Connect1C();
        }

        public IQueryable GetQueryResult(string C1QueryText, _1CEntParameter[] paramList = null)
        {
            IQueryable result = null;
            if (paramList == null)
                result = connectionUt.ReturnTable(C1QueryText).AsEnumerable().AsQueryable();
            else
                result = connectionUt.ReturnTable(C1QueryText, paramList).AsEnumerable().AsQueryable();


            //connectionUt = null;
            return result;
        }


        public virtual IQueryable GetQueryResult()
        {
            return null;
        }



        public void Dispose()
        {
            if (connectionUt != null) connectionUt.Dispose();
        }
    }
}