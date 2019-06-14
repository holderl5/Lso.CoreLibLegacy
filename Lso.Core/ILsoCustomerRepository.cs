using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public interface ILsoCustomerRepository
    {       
        IList<ILsoCustomer> GetAllWithCustId(int custId);
        IList<ILsoCustomer> GetAllWithCustName(string custName);
        IList<ILsoCustomer> GetAllWithCustIdZipcode(int custId, string zipcode);

        IList<ILsoCustomer> GetAllWithDataLike(string custName, string CustContactName, string BillToAddress1,
                                                      string CustContactPhone, string PhyAddress1);
        IList<ILsoCustomer> GetAllWithCustNameLike(string custName);
        IList<ILsoCustomer> GetAllWithCustContactNameLike(string CustContactName);
        IList<ILsoCustomer> GetAllWithBillToAddress1Like(string BillToAddress1);
        IList<ILsoCustomer> GetAllWithPhyAddress1Like(string PhyAddress1);
        IList<ILsoCustomer> GetAllWithCustContactPhone(string CustContactPhone);        
        void Add(ILsoCustomer item);
        void Update(ILsoCustomer item);


        // TODO: TECHNICAL DEBT - Due to time constraints these SPs are being called from here
        // TODO: though they completely violate the repository pattern
        // TODO: Get rid of this ASAP
        ISingleResult<Lso.Core.sp_GetCustomerBaseDiscountsResult> GetBaseDiscounts(int CustID);
        ISingleResult<Lso.Core.sp_GetCustomerTierRevenueDiscountsResult> GetTieredRevenueDiscounts(int CustID);
        ISingleResult<Lso.Core.sp_GetCustomerWeightDiscountsResult> GetWeightDiscounts(int CustID);
        ISingleResult<Lso.Core.sp_GetCustomerZoneDiscountsResult> GetZoneDiscounts(int CustID);
    }
}
