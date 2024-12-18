using ReactFlight.Server.Model.Product;
using ReactFlight.Server.Model.User;
using System.ComponentModel;
using System.Data;

namespace ReactFlight.Server.BussinessCore.Common
{
    public static class TableCreation
    {
        #region Table for Flight Details
        public static DataTable GetTableForFlightDetails(ItineraryDTO objItinerary)
        {
            DataTable dtFlightDetails = new DataTable();
            dtFlightDetails.Columns.Add("IBE_BOK_MST_Booking_ref", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_BOK_DTL_Prod_Booking_ID", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Carier_Name", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_From_Destination", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_From_Date_Time", Type.GetType("System.DateTime"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_To_Destination", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_To_Date_Time", Type.GetType("System.DateTime"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Flight_No", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Class", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Status", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Fare_Basis", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Journy_Direction", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Not_Valid_Befor", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Not_Valid_After", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Baggage_Allownce", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Airport_Terminal", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Seg_ID", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Fare_Guar_Code", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Seg_Remarks", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_IsSegRemark_VisibleToCust", Type.GetType("System.Boolean"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Codeshare_Operating_Carrier", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_Codeshare_Operating_Flight_No", Type.GetType("System.String"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_ChangeOfPlane", Type.GetType("System.Boolean"));
            dtFlightDetails.Columns.Add("IBE_TVL_DTL_NumberOfStops", Type.GetType("System.Int16"));

            foreach (var objSegment in objItinerary.AirSegments ?? new())
            {
                DataRow dr = dtFlightDetails.NewRow();
                dr["IBE_BOK_MST_Booking_ref"] = objItinerary.BookingRef;
                dr["IBE_BOK_DTL_Prod_Booking_ID"] = objItinerary.ProdBookingId;
                dr["IBE_TVL_DTL_Carier_Name"] = objSegment.Carrier?.ToUpper();
                dr["IBE_TVL_DTL_From_Destination"] = objSegment.Origin?.ToUpper();
                dr["IBE_TVL_DTL_From_Date_Time"] = Convert.ToDateTime(objSegment.DepartDate);
                dr["IBE_TVL_DTL_To_Destination"] = objSegment.Destination?.ToUpper();
                dr["IBE_TVL_DTL_To_Date_Time"] = Convert.ToDateTime(objSegment.ArrivalDate);
                dr["IBE_TVL_DTL_Flight_No"] = objSegment.FlightNumber;
                dr["IBE_TVL_DTL_Class"] = objSegment.SubClass?.ToUpper();
                dr["IBE_TVL_DTL_Status"] = objSegment.Status?.ToUpper();
                dr["IBE_TVL_DTL_Fare_Basis"] = string.IsNullOrEmpty(objSegment.FareBasis) ? "N/A" : objSegment.FareBasis.ToUpper();
                dr["IBE_TVL_DTL_Journy_Direction"] = "N/A";
                dr["IBE_TVL_DTL_Not_Valid_Befor"] = "N/A";
                dr["IBE_TVL_DTL_Not_Valid_After"] = "N/A";
                if (string.IsNullOrEmpty(objSegment.BaggageDetails))
                {
                    dr["IBE_TVL_DTL_Baggage_Allownce"] = "N/A";
                }
                else
                {
                    dr["IBE_TVL_DTL_Baggage_Allownce"] = objSegment.BaggageDetails;
                }
                dr["IBE_TVL_DTL_Airport_Terminal"] = string.IsNullOrEmpty(objSegment.AirportTerminal) ? "N/A" : objSegment.AirportTerminal.ToUpper();
                dr["IBE_TVL_DTL_Seg_ID"] = objItinerary.AirSegments?.IndexOf(objSegment) + 1;
                dr["IBE_TVL_DTL_Fare_Guar_Code"] = string.Empty;
                dr["IBE_TVL_DTL_Seg_Remarks"] = objSegment.SegmentRemarks;
                dr["IBE_TVL_DTL_IsSegRemark_VisibleToCust"] = objSegment.IsSegRemarkVisibleToCust;
                if (objSegment.OperatingFlightNumber == null || objSegment.OperatingCarrier == null)
                {
                    dr["IBE_TVL_DTL_Codeshare_Operating_Flight_No"] = ""; ;
                    dr["IBE_TVL_DTL_Codeshare_Operating_Carrier"] = "";
                }
                else
                {
                    dr["IBE_TVL_DTL_Codeshare_Operating_Flight_No"] = objSegment.OperatingFlightNumber;
                    dr["IBE_TVL_DTL_Codeshare_Operating_Carrier"] = objSegment.OperatingCarrier;
                }

                dr["IBE_TVL_DTL_ChangeOfPlane"] = false;
                dr["IBE_TVL_DTL_NumberOfStops"] = objSegment.NoOfStops;
                dtFlightDetails.Rows.Add(dr);
            }
            return dtFlightDetails;
        }
        #endregion
        #region Table for Pax Details
        public static DataTable GetTableForPaxDetails(ItineraryDTO objItinerary)
        {
            DataTable dtPaxDetails = new DataTable();
            dtPaxDetails.Columns.Add("IBE_BOK_MST_Booking_ref", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_BOK_DTL_Prod_Booking_ID", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_Pax_ID", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_Title", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_Pax_First_Name", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_Pax_Middle_Name", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_Pax_Last_Name", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_Age_Group", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_IsLead_Name", Type.GetType("System.Boolean"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_Frequent_Flyer_No", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_PP_Passport_No", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_PP_Nationality", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_PP_Expiry_Date", Type.GetType("System.DateTime"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_Place_of_Birth", Type.GetType("System.String"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_Pax_DOB", Type.GetType("System.DateTime"));
            dtPaxDetails.Columns.Add("IBE_PAX_DTL_Pax_Gender", Type.GetType("System.String"));

            foreach (var objPax in objItinerary.PaxInfos)
            {
                DataRow dr = dtPaxDetails.NewRow();
                dr["IBE_BOK_MST_Booking_ref"] = objItinerary.BookingRef;
                dr["IBE_BOK_DTL_Prod_Booking_ID"] = objItinerary.ProdBookingId;
                dr["IBE_PAX_DTL_Pax_ID"] = objItinerary.PaxInfos.IndexOf(objPax) + 1;
                dr["IBE_PAX_DTL_Title"] = objPax.Title;
                dr["IBE_PAX_DTL_Pax_First_Name"] = objPax.FirstName?.Trim();
                dr["IBE_PAX_DTL_Pax_Middle_Name"] = string.Empty;
                dr["IBE_PAX_DTL_Pax_Last_Name"] = objPax.LastName?.Trim();
                dr["IBE_PAX_DTL_Age_Group"] = objPax.PaxType;
                dr["IBE_PAX_DTL_IsLead_Name"] = objPax.IsLeadName == null ? false : true;
                dr["IBE_PAX_DTL_Frequent_Flyer_No"] = string.Empty;
                dr["IBE_PAX_DTL_PP_Passport_No"] = string.Empty;
                dr["IBE_PAX_DTL_PP_Nationality"] = string.Empty;
                dr["IBE_PAX_DTL_PP_Expiry_Date"] = Convert.ToDateTime("1/1/1900 00:00:00");
                dr["IBE_PAX_DTL_Place_of_Birth"] = string.Empty;
                dr["IBE_PAX_DTL_Pax_DOB"] = objPax.PaxDOB == null ? Convert.ToDateTime("1/1/1900 00:00:00") : Convert.ToDateTime(objPax.PaxDOB);
                if (!string.IsNullOrEmpty(objPax.Gender))
                {
                    if (objPax.Gender.ToUpper() == "MALE") { objPax.Gender = "M"; } else if (objPax.Gender.ToUpper() == "FEMALE") { objPax.Gender = "F"; } else { objPax.Gender = "M"; }//New code to handle gender
                }
                dr["IBE_PAX_DTL_Pax_Gender"] = objPax.Gender;
                dtPaxDetails.Rows.Add(dr);
            }
            return dtPaxDetails;
        }
        #endregion
    }
}
