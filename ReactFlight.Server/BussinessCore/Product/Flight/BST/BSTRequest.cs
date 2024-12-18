using ReactFlight.Server.BussinessCore.Common;
using ReactFlight.Server.Model;
using System;
using System.Text;

namespace ReactFlight.Server.BussinessCore.Product.Flight.BST
{
    public static class BSTRequest
    {
        #region BSTSearch Request
        public static string FlightSearchRequest(RequestModel objRequest)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            if (string.IsNullOrEmpty(objRequest.TripType))
            {
                sb.Append("\"TripType\": \"RT\",");
            }
            else
            {
                sb.Append("\"TripType\": \"" + objRequest.TripType + "\",");
            }

            sb.Append("\"Origin\": \"" + objRequest.Origin + "\",");
            sb.Append("\"Destination\": \"" + objRequest.Destination + "\",");
            sb.Append("\"DepartDate\": \"" + Convert.ToDateTime(objRequest.DepartDate).ToString("dd MMM yyyy") + "\",");
            if (objRequest.TripType == MyEnum.TripType.RT.ToString())
            {
                sb.Append("\"ArrivalDate\": \"" + Convert.ToDateTime(objRequest.ArrivalDate).ToString("dd MMM yyyy") + "\",");
            }
            if (string.IsNullOrEmpty(objRequest.Class))
            {
                sb.Append("\"Class\": \"Economy\",");
            }
            else
            {
                sb.Append("\"Class\": \"" + objRequest.Class + "\",");
            }
            sb.Append(objRequest.IsFlexibleDate ?? false ? "\"IsFlexibleDate\": true," : "\"IsFlexibleDate\": false,");
            sb.Append(objRequest.IsDirectFlight ?? false ? "\"IsDirectFlight\": true," : "\"IsFlexibleDate\": false,");
            sb.Append("\"NoOfInfantPax\": \"" + objRequest.NoOfInfantPax + "\",");
            sb.Append("\"NoOfAdultPax\": \"" + objRequest.NoOfAdultPax + "\",");
            sb.Append("\"NoOfChildPax\": \"" + objRequest.NoOfChildPax + "\",");
            sb.Append("\"NoOfYouthPax\": \"" + objRequest.NoOfYouthPax + "\",");
            sb.Append("\"CompanyCode\": \"" + objRequest.CompanyCode + "\",");
            if (!string.IsNullOrEmpty(objRequest.AirlineCode))
            {
                sb.Append("\"AirlineCode\": \"" + objRequest.AirlineCode + "\",");
            }
            sb.Append("\"WebsiteName\": \"" + objRequest.WebsiteName + "\"");

            sb.Append("}");

            return sb.ToString();
        }
        #endregion

        #region BSTPricing Request
        public static string PricingRequest(RequestModel objRequest)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"Token\": \"" + objRequest.Token + "\",");
            sb.Append("\"Key\": \"" + objRequest.Key + "\",");
            sb.Append("\"TripType\": \"" + objRequest.TripType + "\",");
            sb.Append("\"AccountCode\": \"" + objRequest.AccountCode + "\",");
            sb.Append("\"OutBoundKey\": \"" + objRequest.OutBoundKey + "\",");
            sb.Append("\"InboundKey\": \"" + objRequest.InBoundKey + "\",");
            sb.Append("\"supp\": \"" + objRequest.Supp + "\",");
            if (objRequest.IsFlexibleDate ?? false)
            {
                sb.Append("\"IsFlexibleDate\": true,");
            }
            else
            {
                sb.Append("\"IsFlexibleDate\": false,");
            }
            sb.Append("\"NoOfInfantPax\": \"" + objRequest.NoOfInfantPax + "\",");
            sb.Append("\"NoOfAdultPax\": \"" + objRequest.NoOfAdultPax + "\",");
            sb.Append("\"NoOfChildPax\": \"" + objRequest.NoOfChildPax + "\",");
            sb.Append("\"NoOfYouthPax\": \"" + objRequest.NoOfYouthPax + "\",");
            sb.Append("\"CompanyCode\": \"" + objRequest.CompanyCode + "\",");
            sb.Append("\"WebsiteName\": \"" + objRequest.WebsiteName + "\",");
            sb.Append("\"OptionKeyList\": [");
            foreach (var optionKey in objRequest.OptionKeyList)
            {
                sb.Append("\"" + optionKey + "\",");
            }
            sb.Length--; 
            sb.Append("]");
            sb.Append("}");
            return sb.ToString();
        }
        #endregion

        //#region PNRRequest
        //public string CreatePNRRequest(ItineraryDTO objRequest)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("{");
        //    sb.Append("\"Token\": \"" + objRequest.Token + "\",");
        //    sb.Append("\"Key\": \"" + objRequest.Key + "\",");
        //    sb.Append("\"TripType\": \"" + objRequest.TripType + "\",");
        //    sb.Append("\"BookingCode\": \"" + objRequest.BookingCode + "\",");
        //    sb.Append("\"AccountCode\": \"" + objRequest.AccountCode + "\",");
        //    sb.Append("\"CompanyCode\": \"" + objRequest.CompanyCode + "\",");
        //    sb.Append("\"WebsiteName\": \"" + objRequest.WebsiteName + "\",");
        //    sb.Append("\"Supp\": \"" + objRequest.Supp + "\", ");
        //    sb.Append("\"Pax\": [");

        //    // Iterate through Pax array
        //    foreach (var pax in objRequest.Pax)
        //    {
        //        sb.Append("{");
        //        sb.Append("\"Title\": \"" + pax.Title + "\",");
        //        sb.Append("\"FirstName\": \"" + pax.FirstName + "\",");
        //        sb.Append("\"MiddleName\": \"" + pax.MiddelName + "\",");
        //        sb.Append("\"LastName\": \"" + pax.LastName + "\",");
        //        sb.Append("\"PaxType\": \"" + pax.PaxType + "\",");
        //        sb.Append("\"Gender\": \"" + pax.Gender + "\",");
        //        sb.Append("\"PaxDOB\": \"" + pax.PaxDOB + "\",");
        //        sb.Append("\"IsLeadName\": " + (pax.IsLeadName != null ? (bool)pax.IsLeadName ? "true" : "false" : "false"));
        //        sb.Append("}");

        //        // Add comma if not the last element
        //        if (pax != objRequest.Pax.Last())
        //        {
        //            sb.Append(",");
        //        }
        //    }

        //    sb.Append("],");

        //    sb.Append("\"AddressInfo\": {");
        //    sb.Append("\"City\": {");
        //    sb.Append("\"CityCode\": \"" + objRequest.AddressInfo.City.CityCode + "\",");
        //    sb.Append("\"AreaCode\": \"" + objRequest.AddressInfo.City.AreaCode + "\",");
        //    sb.Append("\"CityName\": \"" + objRequest.AddressInfo.City.CityName + "\",");
        //    sb.Append("\"BillingCityName\": \"" + objRequest.AddressInfo.City.BillingCityName + "\"},");

        //    sb.Append("\"Country\": {");
        //    sb.Append("\"CountryCode\": \"" + objRequest.AddressInfo.Country.CountryCode + "\",");
        //    sb.Append("\"CountryName\": \"" + objRequest.AddressInfo.Country.CountryName + "\",");
        //    sb.Append("\"BillingCountryName\": \"" + objRequest.AddressInfo.Country.BillingCountryName + "\"},");

        //    sb.Append("\"Street\": {");
        //    sb.Append("\"HouseNo\": \"" + objRequest.AddressInfo.Street.HouseNo + "\",");
        //    sb.Append("\"PostalCode\": \"" + objRequest.AddressInfo.Street.PostalCode + "\",");
        //    sb.Append("\"Address1\": \"" + objRequest.AddressInfo.Street.Address1 + "\",");
        //    sb.Append("\"Address2\": \"" + objRequest.AddressInfo.Street.Address2 + "\",");
        //    sb.Append("\"Address3\": \"" + objRequest.AddressInfo.Street.Address3 + "\",");
        //    sb.Append("\"AddressType\": \"" + objRequest.AddressInfo.Street.AddressType + "\",");
        //    sb.Append("\"BillingHouseNo\": \"" + objRequest.AddressInfo.Street.BillingHouseNo + "\",");
        //    sb.Append("\"BillingAddress1\": \"" + objRequest.AddressInfo.Street.BillingAddress1 + "\",");
        //    sb.Append("\"BillingAddress2\": \"" + objRequest.AddressInfo.Street.BillingAddress2 + "\",");
        //    sb.Append("\"BillingZipcode\": \"" + objRequest.AddressInfo.Street.BillingZipcode + "\"}},");

        //    sb.Append("\"Email\":\"" + objRequest.Email + "\",");
        //    sb.Append("\"ContactNo\":\"" + objRequest.ContactNo + "\",");
        //    sb.Append("\"CountryDialingCode\":\"" + objRequest.CountryDialingCode + "\",");
        //    sb.Append("\"TicketInfo\":\"" + objRequest.TicketInfo + "\"");

        //    sb.Append("}");

        //    return sb.ToString();
        //}

        //#endregion

        //#region Create Eticket request
        //public string SendEticketRequest(ItineraryDTO objRequest)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("{");
        //    sb.Append("\"AccountInfo\": {");
        //    sb.Append("\"CompanyCode\": \"" + objRequest?.AccountInfo?.CompanyCode + "\",");
        //    sb.Append("\"WebsiteName\": \"" + objRequest?.WebsiteName + "\",");
        //    sb.Append("\"UserInfo\": {");
        //    sb.Append("\"TicketInfos\": [");
        //    sb.Append("{");
        //    sb.Append("\"TicketNumber\": \"" + objRequest?.TicketInfo + "\"");
        //    sb.Append("}");
        //    sb.Append("]");
        //    sb.Append("}");
        //    sb.Append("},");
        //    sb.Append("\"BookingRef\": \"" + objRequest?.BookingRef + "\",");
        //    sb.Append("\"Supp\": \"" + objRequest?.Supp + "\"");
        //    sb.Append("}");
        //    return sb.ToString();
        //}
        //#endregion

        //#region Create PNR Request
        //public string GetPNRRequest(ItineraryDTO objRequest)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("{");
        //    sb.Append("\"AccountInfo\": {");
        //    sb.Append("\"CompanyCode\": \"" + objRequest?.AccountInfo?.CompanyCode + "\",");
        //    sb.Append("\"WebsiteName\": \"" + objRequest?.WebsiteName + "\"");
        //    sb.Append("},");
        //    sb.Append("\"PNRInfo\": {");
        //    sb.Append("\"Recloc\": \"" + objRequest?.PNRInfo?.RecLoc + "\"");
        //    sb.Append("},");
        //    sb.Append("\"BookingRef\": \"" + objRequest?.BookingRef + "\",");
        //    sb.Append("\"Supp\": \"" + objRequest?.Supp + "\"");
        //    sb.Append("}");
        //    return sb.ToString();
        //}
        //#endregion

    }
}
