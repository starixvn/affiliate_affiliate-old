
namespace Fanex.Affiliate.DataObjects
{
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Model;
    /// <summary>
    /// Summary description for Active12BetAccount
    /// </summary>
    public class Active12BetAccount
    {

        /* 
  In "payment request" page, we have to check whether member get activated or not. If not, then..
       1. Get member information from 12bet databases.
       2. Insert member into IBC database (SP: Dot_Dep_CustomerInsMember_Ag)
       3. Update IBC info into 12bet database.
  */


        /* 
             * @Check 12Player Account activated and automaticaly activate 2009-02-17
                In "payment request" page, we have to check whether member get activated or not. If not, then..
                     1. Get member information from 12bet databases.
                     2. Insert member into IBC database (SP: Dot_Dep_CustomerInsMember_Ag)
                     3. Update IBC info into 12bet database.
            */
        public static bool Check12BetUsernameByAffId(int AffId, ref string errMsg)
        {


            string depositUserName = string.Empty;
            bool isActivated = PaymentRequest.Check12BetPlayerAccountActivated(AffId, out depositUserName, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {/*has error */
                return false;
            }

            if (!isActivated) /*this 12 player account hadn't been activated yet */
            {
                return Active12BetUsername(depositUserName, ref errMsg);
            }
            return true;
        }

        public static bool Active12BetUsername(string depositUserName, ref string errMsg)
        {
            DepositCustomerInfo customer = PaymentRequest.Get12BetPlayerDepositInfo(depositUserName);
            if (customer.Success != 0)
            { /* There was an error when get customer info in Deposit DB */
                //  strErrorMsg = "There was an error when get customer info in Deposit DB";
                errMsg = Resources.Translator.Get("errDepositDBConnectFailed");
                return false;
            }
            int ibcCustID = 0;
            string ibcUserName = string.Empty;
            PaymentRequest.Add12BetPlayerAccountToIBC(customer, out ibcCustID, out ibcUserName, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {/*has error */
                return false;
            }
            int intErrCode = 0;
            try
            {
                PaymentRequest.Update12BetPlayerStatusToDeposit(depositUserName, ibcCustID, ibcUserName, out intErrCode, out errMsg);
            }
            catch
            {
                return false;
            }
            switch (intErrCode)
            {
                case 0:
                    break;
                case 101:
                    errMsg =   Resources.Translator.Get("errUserNotFound");
                    return false;
                case 102:
                    errMsg = Resources.Translator.Get("errExistAccount");
                    return false;
                default:
                    return false;                    
            }
            return true;
        }
    }
}