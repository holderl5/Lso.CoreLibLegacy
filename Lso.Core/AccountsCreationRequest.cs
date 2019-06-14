using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class AccountsCreationRequest
    {
        public string RequesterFirstName;
        public string RequesterLastName;
        public string RequesterPhone;
        public string RequesterEmail;

        public string CompanyName;
        public string CompanyPhone;
        public string CompanyPhyAddress1;
        public string CompanyPhyAddress2;
        public string CompanyPhyCity;
		public string CompanyPhyState;
        public string CompanyPhyZip;

        public string CompanyBillAddress1;
        public string CompanyBillAddress2;
        public string CompanyBillCity;
		public string CompanyBillState;
        public string CompanyBillZip;

        public int HowHearAbout;

        public string LoginName;
        public string UserPWD;
        public string Comments;

        public string CCName;
        public string CCNumber;
        public string CCType;
        public short CCExpMonth;
        public Int32 CCExpYear;
        public string VerificationCode;
        public string BillingZip;

        public bool CCOptOut;


    }
}
