using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class DebtData
    {
        [DataMember(Name = "partnerId")]
        public Guid partnerId;
        [DataMember(Name = "customerId")]
        public Guid customerId;
        [DataMember(Name = "transactionNumber")]
        public string transactionNumber;
        [DataMember(Name = "transactionDate")]
        public string transactionDate;
        [DataMember(Name = "transactionSum")]
        public double transactionSum;
        [DataMember(Name = "paymentDate")]
        public string paymentDate;
        [DataMember(Name = "debt")]
        public double debt;
        [DataMember(Name = "overdueDebt")]
        public double overdueDebt;
        [DataMember(Name = "overdueDays")]
        public int overdueDays;
    }
}