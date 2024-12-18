using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReactFlight.Server.InfraLayer.DATA;
using ReactFlight.Server.InfraLayer.DataRepository;
using ReactFlight.Server.Model.Common;

namespace ReactFlight.Server.InfraLayer.Product.Common
{
    public class DLGenerateID : IDisposable
    {
        private DataContext _dataContext;
        private bool _disposed;
        public DLGenerateID()
        {
            _dataContext = new DataContext();
        }
        public string GetBookingRef(string prefix)
        {
            string bookingRef = string.Empty;
            using (IBEEntity? iBEEntity = (IBEEntity?)_dataContext.GetDBContext(MYEnum.Database.IBE))
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@STRPREFIX", prefix),
                    };
                    var result = iBEEntity?.Booking_Ref_IDs.FromSqlRaw("Exec sp_Genrate_Booking_Ref_IDs @STRPREFIX", parameters.ToArray()).AsEnumerable().FirstOrDefault();
                    return result?.BookingReference ?? "";
                }
                catch
                {
                }
            }
            return bookingRef;
        }
        public void Dispose()
        {
            if (_disposed)
            {
                _dataContext = null;
            }
            return;
        }
    }
}
