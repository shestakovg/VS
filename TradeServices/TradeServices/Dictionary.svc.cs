using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TradeServices.Classes;
using TradeServices.DataEntitys;

//dddf
namespace TradeServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Dictionary" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Dictionary.svc or Dictionary.svc.cs at the Solution Explorer and start debugging.
    public class Dictionary : IDictionary
    {
        public  Branch[] GetBranch()
        {
            _1CBranches branches = new _1CBranches();
            return branches.ConvertToArray() as Branch[];
        }


        public Route[] GetRoute(string branchid)
        {
            _1CRoutes branches = new _1CRoutes(branchid);
            return branches.ConvertToArray() as Route[];
        }


        public RouteSet[] GetRouteSet(string routeid)
        {
            _1CRouteSet routeSet = new _1CRouteSet(routeid);
            return routeSet.ConvertToArray() as RouteSet[];

        }


        public Contract[] GetContracts(string routeid)
        {
            _1CContract routeSet = new _1CContract(routeid);
            return routeSet.ConvertToArray() as Contract[]; 
        }


        public SkuGroup[] GetSkuGroup()
        {
            _1CSkuGroup getskugroup = new _1CSkuGroup();
            return getskugroup.ConvertToArray() as SkuGroup[];
        }


        public Sku[] GetSku()
        {
            _1CSku getsku = new _1CSku();
            return getsku.ConvertToArray() as Sku[];
        }


        public Price[] GetPrice(string priceId)
        {
            _1CPrice getprice = new _1CPrice(priceId);
            return getprice.ConvertToArray() as Price[];
        }


        public BalanceSku[] GetBalanceSku(string branchId)
        {
            _1CBalanceSku getprice = new _1CBalanceSku(branchId);
            return getprice.ConvertToArray() as BalanceSku[];
        }
    }
}
