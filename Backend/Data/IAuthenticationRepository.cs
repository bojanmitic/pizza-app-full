using pizza_server.DTOs.User;
using pizza_server.Models;
using System.Threading.Tasks;

namespace pizza_server.Data
{
    public interface IAuthenticationRepository
    {
        Task<ServiceResponse<string>> Register(User user, string password);
        Task<ServiceResponse<ResponseLoginDTO>> Login(string email, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<UserToken>> RenewToken();
        Task<ServiceResponse<string>> ForgotPassword(ForgotPassword email);
        Task<ServiceResponse<string>> UpdatePassword(ResetPassword resetPassword);
    }
}
