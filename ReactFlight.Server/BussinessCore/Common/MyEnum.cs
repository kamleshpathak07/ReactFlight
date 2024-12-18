namespace ReactFlight.Server.BussinessCore.Common
{
    public class MyEnum
    {
        public enum CustomerType
        {
            DICT,
            AGNT,
            INTR,
            CORP
        }
        public enum Currency
        {
            GBP,
            INR,
            EUR
        }
        #region Status
        public enum Status
        {
            INCOMPLETE,
            OPTION,
            HOLD,
            FIRM,
            ISSUED,
            CANCELLED,
            QUEUE,
            REFUND,
            REFUNDQ,
            NONE,
            Confirmed,
            GROUP_FIRM,
            QUEUE_PENDING
        }
        #endregion
        public enum PAXType
        {
            Adult,
            Youth,
            Child,
            Infant,
            ADT,
            YTH,
            CHD,
            CNN,
            INF,
            JWZ,
            AJI,
            GBE,
            CJI,
            JWB,
            IJI,
            VFR,
            VFN,
            VFF,
            JCB,
            JNN,
            JNF,
            ITX,
            INN,
            ITF,
            CH,
            IN,
            TIM,
            TIN,
            TIF,
            TIM13,
            VNN
        }
        public enum TripType
        {
            RT,
            OW,
            MT
        }
        public enum Product
        {
            ARF,
            HTL,
            INS,
            DYN,
            EuroStar,
            YCH,
            PCR,
            TRF,
            CAR,
            TRCZ
        }
        public enum SearchMode
        {
            TEST,
            LIVE
        }
        public enum Method
        {
            POST,
            GET,
            PATCH,
            PUT,
            DELETE
        }
        public enum Supp
        {
            GAL,
            EMRNDC,
            VTLNDC,
            EMRVFR,
            EMRITX,
            AGWNDC,
            NDC,
            JGAL,
            JNDC
        }
        public enum BookingPreFix
        {
            IBE,
            TRA,
            TRN,
            LOG,
            RT,
            PMT
        }
        public enum ChargeType
        {
            FAR,
            TAX,
            ALL,
            SGST,
            CGST,
            IGST,
            RDFH
        }
        public enum PaymentStatus
        {
            DUE,
            OPTION,
            FIRM,
            ISSUED,
            PARTIAL
        }
        public struct Database
        {
            public const string IBE = "IBE", SHELL = "SHELL", BRIGHTSUN = "BRIGHTSUN", BSHOTEL = "BS_HOTEL";
        }
        public struct AccountCode
        {
            public const string DICTAccountCode = "BSDICT";
        }
        public struct Customer
        {
            public const string DICT = "DICT", AGNT = "AGNT", CORP = "CORP", INTR = "INTR";
        }
        public struct FileType
        {
            public const string TXT = ".txt", XML = ".xml", JSON = ".json";
        }
        public struct ApplicationMode
        {
            public const string TEST = "TEST", LIVE = "LIVE";
        }
        #region Booking Media
        public struct BookingMedia
        {
            public const String
                BTResCom = "BTRES",
                BTResCoIn = "BTRES.CO.IN",
                TeleUk = "TELE-UK",
                TeleIn = "TELE-IN",
                TravUk = "TRAV-UK",
                TravIn = "TRAV-IN",
                BrightsunCoUk = "BRIGHTSUN.CO.UK",
                BrightsunCoIn = "BRIGHTSUN.CO.IN",
                MBrightsunCoUk = "M.BRIGHTSUN.CO.UK",
                TraveEasyCom = "TRAVEASY.COM",
                TraveMEasyCom = "M.TRAVEASY.COM",
                TraveEasyCoIn = "TRAVEASY.CO.IN",
                BTpremier = "BTPREMIER",
                WhiteLabel = "WHITE-LABEL",
                BtresUk = "BtresUk",
                BtresdotCom = "BTRES.COM";
        };
        #endregion
        public struct CustomDateTime
        {
            public static DateTime DefaultDateTime = new System.DateTime(1900, 01, 01);
        }
    }
}
