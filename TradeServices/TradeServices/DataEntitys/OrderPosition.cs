using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeServices.DataEntitys
{
    public class OrderPosition
    {
        private Guid skuId;

        public Guid SkuId
        {
            get { return skuId; }
            set { skuId = value; }
        }

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Guid priceId;


        public DateTime finalDate = DateTime.Today;

        public OrderPosition(Guid skuId, int quantity, Guid priceId)
        {
            this.skuId = skuId;
            this.quantity = quantity;
            this.priceId = priceId;
        }
        public OrderPosition(Guid skuId, int quantity, Guid priceId, DateTime finalDate)
        {
            this.skuId = skuId;
            this.quantity = quantity;
            this.priceId = priceId;
            this.finalDate = finalDate;
        }
    }
}