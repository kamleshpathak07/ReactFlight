using Microsoft.EntityFrameworkCore;
using ReactFlight.Server.BLDataRepository.ILData;
using ReactFlight.Server.BussinessCore.Common;
using ReactFlight.Server.InfraLayer.DATA;

namespace ReactFlight.Server.BLDataRepository.BLData
{
    public class BLDataContext : ILDataContext
    {
        private readonly IBEEntity _iBEEntity;
        private readonly SHELLEntity _sHELLEntity;
        private readonly BrightsunEntity _brightsunEntity;
        public BLDataContext(IBEEntity iBEEntity, SHELLEntity sHELLEntity, BrightsunEntity brightsunEntity)
        {
            _iBEEntity = iBEEntity;
            _sHELLEntity = sHELLEntity;
            _brightsunEntity = brightsunEntity;
        }
        public DbContext GetDbContext(string DataBase)
        {
            switch (DataBase)
            {
                case MyEnum.Database.IBE:
                    return (DbContext)_iBEEntity;
                case MyEnum.Database.SHELL:
                    return (DbContext)_sHELLEntity;
                case MyEnum.Database.BRIGHTSUN:
                    return (DbContext)_brightsunEntity;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
