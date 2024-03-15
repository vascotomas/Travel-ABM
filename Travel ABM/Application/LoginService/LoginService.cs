using Application.DTO_s;
using Application.LoginService;
using Application.Utils;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private IConfiguration config;
        public LoginService(ILoginRepository loginRepository/*, IConfiguration configuration */) //Comento esta seccion ya que No puedo acceder al Iconfiguration desde Test
        {
            _loginRepository = loginRepository;
            //config = configuration;
        }
        public async Task<Usuario> Get(UserDto user) => await _loginRepository.Get(user);


        public async Task<string> GenerateToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(/*config.GetSection("JWT:KEY").Value*/"super clave secreta para API"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var securityToken = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(60),
                                signingCredentials: creds);
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public async Task<bool> ForgotPassword(string email)
        {
            var user = await _loginRepository.Get(new UserDto { Email = email });
            if (user == null)
            {
                return false;
            }
            user.PasswordResetToken = Tools.CreateRandomToken();
            user.PasswordResetExpire = DateTime.Now.AddDays(1);
            await _loginRepository.UpdateUserReset(user);

            return true;
        }
        public async Task<bool> ResetPassword(ResetPasswordRequestDto request)
        {
            var user = await _loginRepository.GetByToken(request.Token);
            if (user == null || user.PasswordResetExpire < DateTime.Now)
            {
                return false;
            }
            Tools.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.PasswordResetExpire = null;
            await _loginRepository.UpdateUserReset(user);

            return true;
        }

        public async Task<Tuple<bool, string>> Login(UserDto userRequest)
        {
            var user = await _loginRepository.Get(userRequest);

            if (user == null)
                return Tuple.Create(false, "User not found");
            if (!Tools.VerifyPasswordHash(userRequest.Password, user.PasswordHash, user.PasswordSalt))
                return Tuple.Create(false, "Password incorrect or user ");

            return Tuple.Create(true, "Welcome");


        }

    }
}
