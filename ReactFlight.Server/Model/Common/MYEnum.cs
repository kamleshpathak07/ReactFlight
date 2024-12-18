namespace ReactFlight.Server.Model.Common
{
    public class MYEnum
    {
        
        public struct Customer
        {
            public const string DICT = "DICT", AGNT = "AGNT", CORP = "CORP", INTR = "INTR";
        }
        public struct Database
        {
            public const string IBE = "IBE", SHELL = "SHELL", BRIGHTSUN = "BRIGHTSUN", BSHOTEL = "BS_HOTEL";
        }
        public struct BookingRefPrefix
        {
            public const string IBE = "IBE", TRA = "TRA";
        }
        #region DB Row change status
        public enum DBRowChangeStatus
        {
            SUCCESS,
            ERROR
        }
        #endregion
        public enum ServiceMode
        {
            TEST,
            LIVE
        }
        public enum ProductRef
        {
            AFR,
            HTL,
            TRCZ,
            INS,
            TRF
        }
        public enum Currency
        {
            INR,
            GBP,
            EUR
        }
        public struct FileType
        {
            public const string TXT = ".txt", XML = ".xml", JSON = ".json";
        }
    }
}
