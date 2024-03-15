using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Utils;
using Domain;

namespace Application.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public Task<bool> CrearUsuario(Domain.Usuario user)
        {
            Tools.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return _userRepository.CrearUsuario(user);
        }

        public async Task<List<Domain.Usuario>> ObtenerUsuarios() => await _userRepository.ObtenerUsuarios();
            
        

        public async Task<Domain.Usuario> Obtener(int id)=> await _userRepository.Obtener(id);
        
        
        public async Task<bool> SoftDelete(int id)=>await _userRepository.SoftDelete(id);
        
    }
}
