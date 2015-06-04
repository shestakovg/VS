using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;
namespace TradeServices.Classes
{
    public class _1CSkuGroup: _1CUtManager, IResultArray<SkuGroup>
    {
    
         private _1CUtilsEnterra._1CEntParameter[] paramList;

        #region 1cquery
         private const string _1CQuery = @" ВЫБРАТЬ
	                                            Номенклатура.Ссылка как GroupId,
	                                            Номенклатура.Наименование как GroupName,
	                                            Номенклатура.Родитель как GroupParentId,
	                                            Номенклатура.Родитель.Наименование как GroupParentName
                                            ИЗ
	                                            Справочник.Номенклатура КАК Номенклатура
                                            ГДЕ
	                                            Номенклатура.ЭтоГруппа 
                                        ";
        #endregion
        public SkuGroup[] ConvertToArray(IQueryable<System.Data.DataRow> queryable =  null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<SkuGroup> result =
                    from row in queryable
                    select new SkuGroup()
                    {
                        GroupId = new Guid(row["GroupId"].ToString()),
                        GroupName = row["GroupName"].ToString().Trim(), 
                        GroupParentId = (row["GroupParentId"] == System.DBNull.Value ? Guid.Empty : new Guid(row["GroupParentId"].ToString())),
                        GroupParentName = row["GroupParentName"].ToString().Trim()
                    };
            return result.Cast<SkuGroup>().ToArray();
        }

        public _1CSkuGroup(_1CUtilsEnterra._1CEntParameter[] paramList = null)
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
    
