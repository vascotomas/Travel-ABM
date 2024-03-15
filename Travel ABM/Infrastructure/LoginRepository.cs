using Microsoft.EntityFrameworkCore.Infrastructure;

using Application.LoginService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Application.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class LoginRepository : ILoginRepository
    {
        private readonly TravelContext _context;

        public LoginRepository(TravelContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Get(UserDto user) => await _context.Usuario.SingleOrDefaultAsync(x => x.UserName == user.UserName && x.Password == user.Password || x.Email == user.Email);

        public async Task<Usuario> GetByToken(string token) => await _context.Usuario.SingleOrDefaultAsync(x => x.PasswordResetToken == token);

        public Task UpdateUserReset(Usuario user)
        {
            _context.Usuario.Update(user);
            return _context.SaveChangesAsync();
        }
    }
}
