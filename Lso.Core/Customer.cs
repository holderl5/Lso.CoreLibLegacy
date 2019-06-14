using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public partial class Customer
    {
        partial void OnCreated()
        {            
            DOWBillingCode = "MON2";
            AcctStatus = "A";
            PayMethodID = 'B';
            HeldCycleCount = 0;
            WaivePUFee = false;
            WaiveFuelFee = false;
            OnHoldOverride = false;
            WaiveResDelivFee = false;
            StarCustomer = false;
            WaiveDimWt = false;
            KnownShipper = false;
            BillingRefRequired = false;

        }
    }
}
