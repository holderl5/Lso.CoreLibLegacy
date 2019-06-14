using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class BaseDiscount
    {
        public char ServiceID;
        public decimal DiscountPercent;                
    }

    public class WeightDiscount
    {
        public char ServiceID;                
        public decimal StartWeight;
        public decimal EndWeight;
        public decimal DiscountPercent;
    }

    public class ZoneDiscount
    {
        public char ServiceID;
        public int Zone;
        public decimal DiscountPercent;
    }

    public class TieredRevenueDiscount
    {
        public char ServiceID;
        public decimal RevenueCeiling;
        public decimal DiscountPercent;        
        
    }

    public class FullCustomerDataResponse
    {
        public bool Success;
        public string ErrMessage;

        public ILsoCustomer CustRecord;

        public List<IWebshipAccount> UserProfiles;
        public List<BaseDiscount> BaseDiscounts;
        public List<WeightDiscount> WeightDiscounts;
        public List<ZoneDiscount> ZoneDiscounts;
        public List<TieredRevenueDiscount> TieredRevenueDiscounts;
    }
}
