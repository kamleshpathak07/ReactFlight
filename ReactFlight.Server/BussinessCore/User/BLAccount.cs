using ReactFlight.Server.BLDataRepository.ILData;
using ReactFlight.Server.InfraLayer.Product.User;
using ReactFlight.Server.Model.User;

namespace ReactFlight.Server.BussinessCore.User
{
    public class BLAccount
    {
        private readonly DLAccount _dLAccount;
        private readonly ILDataContext _dataContext;
        public BLAccount(ILDataContext lDataContext)
        {
            _dLAccount = new DLAccount();
            _dataContext = lDataContext;
        }
        public BLAccount()
        {
            _dLAccount = new DLAccount();
        }
        public List<UserDTO> GetAccountInfo(UserDTO userDTO)
        {
            return _dLAccount.GetUserDetails(userDTO);
        }
    }
}
