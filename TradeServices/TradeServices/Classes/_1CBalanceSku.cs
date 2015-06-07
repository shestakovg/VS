using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TradeServices.DataEntitys;
using TradeServices.Interfaces;

namespace TradeServices.Classes
{
    public class _1CBalanceSku : _1CUtManager, IResultArray<BalanceSku>
    {

        private _1CUtilsEnterra._1CEntParameter[] paramList = null;

        #region 1cquery
        private const string _1CQuery = @"   
                                            выбрать    
                                            С.Номенклатура как SkuId,
                                            сумма (с.остгл) как StockG,
                                            сумма (с.остроз) как StockR
                                            из (
		                                                 ВЫБРАТЬ
			                                            СвободныеОстаткиОстатки.Номенклатура,
			                                            СвободныеОстаткиОстатки.ВНаличииОстаток - СвободныеОстаткиОстатки.ВРезервеОстаток КАК остгл,
			                                                0 как остроз
		                                            ИЗ
			                                            РегистрНакопления.СвободныеОстатки.Остатки(, Склад.Наименование = ""Главный склад"") КАК СвободныеОстаткиОстатки
		                                            
		                                          union 
		                                               ВЫБРАТЬ
			                                            СвободныеОстаткиОстатки.Номенклатура, 
			                                            0	 КАК остгл ,
			                                            СвободныеОстаткиОстатки.ВНаличииОстаток - СвободныеОстаткиОстатки.ВРезервеОстаток КАК остроз

		                                            ИЗ
			                                            РегистрНакопления.СвободныеОстатки.Остатки(, Склад.Наименование = ""Розница"") КАК СвободныеОстаткиОстатки	
	                                             ) с
                                            сгруппировать по С.Номенклатура	
                                        ";
        #endregion
    public BalanceSku[] ConvertToArray(IQueryable<System.Data.DataRow> queryable =  null)
        {
            if (queryable == null)
            {
                queryable = this.GetQueryResult() as IQueryable<DataRow>;
            }

            IEnumerable<BalanceSku> result =
                    from row in queryable
                    select new BalanceSku()
                    {
                        SkuId = new Guid(row["SkuId"].ToString()),
                        StockG = double.Parse(row["StockG"].ToString()),
                        StockR = double.Parse(row["StockR"].ToString())
                    };
            return result.Cast<BalanceSku>().ToArray();
        }

    public _1CBalanceSku(string branchId)
            : base() 
        {
            if (!String.IsNullOrEmpty(branchId))
            {
                //var entParameter = new _1CUtilsEnterra._1CEntParameter();
                //entParameter.AddCatalogReferenceParameterByID(this.ConnectionUt, "Организации", branchId, "branchId");
                //this.paramList = new _1CUtilsEnterra._1CEntParameter[1] { entParameter };
            }
            else
            {
                throw new Exception("branchId is empty");
            }
        }
        public override IQueryable GetQueryResult()
        {

            return base.GetQueryResult(_1CQuery, this.paramList);
        }
    }
}        
