namespace Fanex.Affiliate.Model
{
    using System;

    /// <summary>
    /// Summary description for DepositCustomerInfo
    /// </summary>
    public class DepositCustomerInfo
    {
        public DepositCustomerInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int _Success;
        private string _FirstName;
        private string _LastName;
        private string _Email;
        private string _DepositUserName;
        private int _DepositCustID;
        private string _Password;
        private string _OddsType;
        private int _AgentID;
        private int _Currency;
        private int _AffiliateId;
        private int _AgentType;
        private string _Address;
        private string _City;
        private DateTime _Birthday;
        private string _Country;
        private string _Phone;
        private int _Gender;
        private string _State;
        private string _ZipCode;
        private int _CasinoFlag;
        private string _BonusCode;
        private int _DepositUserId;

        public int Success
        {
            get
            {
                return _Success;
            }
            set
            {
                _Success = value;
            }

        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string Email { get { return _Email; } set { _Email = value; } }
        public string DepositUserName { get { return _DepositUserName; } set { _DepositUserName = value; } }
        public int DepositCustID { get { return _DepositCustID; } set { _DepositCustID = value; } }
        public string Password { get { return _Password; } set { _Password = value; } }
        public string OddsType { get { return _OddsType; } set { _OddsType = value; } }
        public int AgentID { get { return _AgentID; } set { _AgentID = value; } }
        public int Currency { get { return _Currency; } set { _Currency = value; } }
        public int AffiliateId { get { return _AffiliateId; } set { _AffiliateId = value; } }
        public int AgentType { get { return _AgentType; } set { _AgentType = value; } }
        public string Address { get { return _Address; } set { _Address = value; } }
        public string City { get { return _City; } set { _City = value; } }
        public DateTime Birthday { get { return _Birthday; } set { _Birthday = value; } }
        public string Country { get { return _Country; } set { _Country = value; } }
        public string Phone { get { return _Phone; } set { _Phone = value; } }
        public int Gender { get { return _Gender; } set { _Gender = value; } }
        public string State { get { return _State; } set { _State = value; } }
        public string ZipCode { get { return _ZipCode; } set { _ZipCode = value; } }
        public int CasinoFlag { get { return _CasinoFlag; } set { _CasinoFlag = value; } }
        public string BonusCode { get { return _BonusCode; } set { _BonusCode = value; } }
        public int DepositUserId { get { return _DepositUserId; } set { _DepositUserId = value; } }

    }
}