using ReactFlight.Server.InfraLayer.Product.Common;

namespace ReactFlight.Server.BussinessCore.Common
{
    public class BLGenrateID
    {
        private readonly DLGenerateID _dLGenerateID;
        public BLGenrateID()
        {
            _dLGenerateID = new DLGenerateID();
        }
        public string GetBookingRefIds(string prefix)
        {
            return _dLGenerateID.GetBookingRef(prefix);
        }
    }
}
