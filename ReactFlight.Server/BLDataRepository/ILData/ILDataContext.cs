using Microsoft.EntityFrameworkCore;

namespace ReactFlight.Server.BLDataRepository.ILData
{
    public interface ILDataContext
    {
        public DbContext GetDbContext(string DataBase);
    }
}
