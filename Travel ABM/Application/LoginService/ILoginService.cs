using Application.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LoginService
{
    public interface ILoginService
    {
        Task<Domain.Usuario> Get(UserDto user);
        Task<string> GenerateToken(Domain.Usuario user);
        Task<bool> ForgotPassword(string email);
        Task<bool> ResetPassword(ResetPasswordRequestDto request);
        Task<Tuple<bool, string>> Login(UserDto user);
    }
}
