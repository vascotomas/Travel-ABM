using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using Services.DTO_s;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private IConfiguration config;
        private readonly Tools _tools;
        public LoginService(ILoginRepository loginRepository, IConfiguration configuration, Tools tools)
        {
            _loginRepository = loginRepository;
            config = configuration;
            _tools = tools;
        }
        public UserCrud Get(UserCrudDto user)
        {
            return _loginRepository.Get(user);
        }
        
        public string GenerateToken(UserCrud user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:KEY").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var securityToken = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(60),
                                signingCredentials: creds);
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public bool ForgotPassword(string email)
        {
            var user = _loginRepository.Get(new UserCrudDto { Email = email});
            if( user == null )
            {
                return false;
            }
            user.PasswordResetToken = _tools.CreateRandomToken();
            user.PasswordResetExpire = DateTime.Now.AddDays(1);
            _loginRepository.UpdateUserReset(user);

            return true;
        }
        public bool ResetPassword(ResetPasswordRequestDto request)
        {
            var user = _loginRepository.GetByToken(request.Token);
            if (user == null || user.PasswordResetExpire < DateTime.Now)
            {
                return false;
            }
            _tools.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.PasswordResetExpire = null;
            _loginRepository.UpdateUserReset(user); 

            return true;
        }

        public Tuple<bool,string> Login(UserCrudDto userRequest)
        {
            var user = _loginRepository.Get(userRequest);

            if (user == null)
                return Tuple.Create(false, "User not found");
            if (!_tools.VerifyPasswordHash(userRequest.Password, user.PasswordHash, user.PasswordSalt))
                return Tuple.Create(false, "Password incorrect or user ");

            return Tuple.Create(true, "Welcome");
            

        }

    }
}
