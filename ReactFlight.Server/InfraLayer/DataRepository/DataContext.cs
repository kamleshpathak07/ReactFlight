using Microsoft.EntityFrameworkCore;
using ReactFlight.Server.InfraLayer.DATA;
using ReactFlight.Server.Model.Common;

namespace ReactFlight.Server.InfraLayer.DataRepository
{
    public class DataContext
    {
        public DbContext? DBContext { get; set; }
        public DbContext? GetDBContext(string database)
        {
            string? databaseurl = null;
            var configurationroot = (ConfigurationRoot)new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionSection = (configurationroot.GetValue<string>("ServiceMode") ?? string.Empty).Equals(MYEnum.ServiceMode.TEST.ToString(), StringComparison.OrdinalIgnoreCase) ? "TestConnection" : "LiveConnection";
            switch (database)
            {
                case MYEnum.Database.BRIGHTSUN:
                    databaseurl = configurationroot.GetSection("ConnectionString").GetSection(connectionSection)["BrightsunEntity"];
                    var optionsBuilderBrightSun = new DbContextOptionsBuilder<BrightsunEntity>();
                    optionsBuilderBrightSun.UseSqlServer(databaseurl);
                    return new BrightsunEntity(optionsBuilderBrightSun.Options);
                case MYEnum.Database.IBE:
                    databaseurl = configurationroot.GetSection("ConnectionString").GetSection(connectionSection)["IBEEntity"];
                    var optionsBuilderIBE = new DbContextOptionsBuilder<IBEEntity>();
                    optionsBuilderIBE.UseSqlServer(databaseurl);
                    return new IBEEntity(optionsBuilderIBE.Options);
                case null:
                    return null;
            }
            return DBContext;
        }
        public DbContext? GetDBContext(string database, string searchMode)
        {
            string? databaseurl = null;
            var configurationroot = (ConfigurationRoot)new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionSection = searchMode.Equals(MYEnum.ServiceMode.TEST.ToString(), StringComparison.OrdinalIgnoreCase) ? "TestConnection" : "LiveConnection";
            switch (database)
            {
                case MYEnum.Database.BRIGHTSUN:
                    databaseurl = configurationroot.GetSection("ConnectionString").GetSection(connectionSection)["BrightsunEntity"];
                    var optionsBuilderBrightSun = new DbContextOptionsBuilder<BrightsunEntity>();
                    optionsBuilderBrightSun.UseSqlServer(databaseurl);
                    return new BrightsunEntity(optionsBuilderBrightSun.Options);
                case MYEnum.Database.IBE:
                    databaseurl = configurationroot.GetSection("ConnectionString").GetSection(connectionSection)["IBEEntity"];
                    var optionsBuilderIBE = new DbContextOptionsBuilder<IBEEntity>();
                    optionsBuilderIBE.UseSqlServer(databaseurl);
                    return new IBEEntity(optionsBuilderIBE.Options);
                case null:
                    return null;
            }
            return DBContext;
        }
    }
}
