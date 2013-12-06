namespace Fanex.Affiliate.Core.Admin.GenerateHTML
{
    using System.Collections;
    using System.Data;
    using DNAide.Data;
    public class Domain
    {
	    public Domain()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        public static string GenCountryList(string selectedMarket, bool hasAll = false)
        {
            DbResult rs = DataObjects.Domain.CountrySel();
            ArrayList arrText = new ArrayList();
            ArrayList arrValue = new ArrayList();

            if (hasAll)
            {
                arrText.Add(Fanex.Affiliate.Resources.Admin.All);
                arrValue.Add("");
            }

            foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
            {
                arrText.Add(dr["CountryName"]);
                arrValue.Add(dr["CountryId"]);
            }
            return AppCommon.GenerateSelectObj("slCountry", arrValue, arrText, selectedMarket);
        }

        public static string GenAffDomainList(string selectedDomain, bool hasAll = false)
        {
            DbResult rs = DataObjects.Domain.DomainAffSel();
            ArrayList arrText = new ArrayList();
            ArrayList arrValue = new ArrayList();
            
            if (hasAll)
            {
                arrText.Add(Fanex.Affiliate.Resources.Admin.All);
                arrValue.Add("");
            }
            foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
            {
                arrText.Add(dr["DomainName"]);
                arrValue.Add(dr["DomainId"]);
            }
            return AppCommon.GenerateSelectObj("slAffDomain", arrValue, arrText, selectedDomain);
        }

        public static string Gen12BetDomainList(string selectedDomain, bool hasAll = false)
        {
            DbResult rs = DataObjects.Domain.GetListMemberDomain();
            ArrayList arrText = new ArrayList();
            ArrayList arrValue = new ArrayList();
            if (hasAll)
            {
                arrText.Add(Fanex.Affiliate.Resources.Admin.All);
                arrValue.Add("");
            }

            foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
            {
                arrText.Add(dr["DomainName"]);
                arrValue.Add(dr["DomainId"]);
            }
            return AppCommon.GenerateSelectObj("slMemberDomain", arrValue, arrText, selectedDomain);
        }
    }
}