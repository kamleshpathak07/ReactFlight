using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReactFlight.Server.InfraLayer.DATA;
using ReactFlight.Server.InfraLayer.DataRepository;
using ReactFlight.Server.Model.Common;
using ReactFlight.Server.Model.Product.Flight;

namespace ReactFlight.Server.InfraLayer.Product.Flight
{
    public class DLAirport : IDisposable
    {
        private DataContext _dataContext;
        private bool _disposed;
        public DLAirport()
        {
            _dataContext = new DataContext();
        }
        public List<AirportDTO> Get_Airport_AutoComplete(string prefix)
        {
            List<AirportDTO> listAirport = new List<AirportDTO>();
            using (BrightsunEntity? brightsunEntity = (BrightsunEntity?)_dataContext.GetDBContext(MYEnum.Database.BRIGHTSUN))
            {
                try
                {
                    List<SqlParameter> parameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@prefixText", prefix),
                        new SqlParameter("@AppName",DBNull.Value)
                    };
                    var result = brightsunEntity?.airport_AutoCompletes.FromSqlRaw("Exec GET_Airport_AutoComplete_Web @prefixText,@AppName", parameters.ToArray()).ToList().Select(z => new AirportDTO()
                    {
                        AirportCode = z.AirportCode,
                        AirportName = z.AirportName,
                        City = new CityDTO
                        {
                            CityName = z.CityName,
                        },
                        Country = new CountryDTO
                        {
                            CountryCode = z.CountryCode,
                            CountryName = z.CountryName,
                        }
                    }).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    _disposed = true;
                }
            }
            return listAirport;
        }
        public void Dispose()
        {
            if (_disposed)
            {
                _dataContext = null;
            }
            return;
        }
        public void Dispose(bool dispose)
        {
            if (dispose)
                return;
        }
        ~DLAirport()
        {
            Dispose();
        }
    }
}
