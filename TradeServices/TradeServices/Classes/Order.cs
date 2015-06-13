using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TradeServices.DataEntitys
{
  

    public class Order
    {
        protected Guid orderUUID;
        protected long orderid;
        protected Guid outletId;
        protected DateTime orderDate;
        protected string notes;
        protected int payType;
        protected int autoLoad;
        protected Guid _1CDocId;

        protected OrderPosition[] positions;

    }
}