using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReactFlight.Server.InfraLayer.DATA;
using ReactFlight.Server.InfraLayer.DataRepository;
using ReactFlight.Server.Model.Common;
using ReactFlight.Server.Model.Product.Flight;

namespace ReactFlight.Server.InfraLayer.Product.Flight
{
    public class DLAirline : IDisposable
    {
        private DataContext _dataContext;
        private bool _disposed;
        public DLAirline()
        {
            _dataContext = new DataContext();
        }
        public List<AirlineDTO> GetAllAirlineLogo()
        {
            using (BrightsunEntity? brightsunEntity = (BrightsunEntity?)_dataContext.GetDBContext(MYEnum.Database.BRIGHTSUN))
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@AIRLINENM ", DBNull.Value),
                        new SqlParameter("@AIRLINECD",DBNull.Value),
                        new SqlParameter("@AIRLINETHREEDGTCD", DBNull.Value)
                    };
                    var result = brightsunEntity?.BS_Airline_Details.FromSqlRaw("Exec BS_Fares_Airlines_Get @AIRLINENM,@AIRLINECD,@AIRLINETHREEDGTCD", parameters.ToArray()).ToList().Select(z => new AirlineDTO()
                    {
                        AirlineCode = z.AirlineCode ?? "",
                        AirlineName = z.AirlineName ?? "",
                        AirlineLogo = z.AirlineLogo ?? ""
                    }).ToList();
                    return result ?? new();
                }
                catch (Exception ex)
                {
                    string message = $"{ex.Message} - {ex.StackTrace}";
                    _disposed = true;
                }
            }
            return new List<AirlineDTO>();
        }
        public void Dispose()
        {
            //_dataContext = null;
            return;
        }
    }
}
