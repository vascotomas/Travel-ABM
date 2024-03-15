
using Application.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly TravelContext _context;

        public UserRepository(TravelContext context)
        {
            _context = context;
        }

        public async Task<bool> CrearUsuario(Usuario usuario)
        {
            try
            {
                _context.Usuario.Add(usuario);
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task<bool> SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> Obtener(int id) => await _context.Usuario.FirstOrDefaultAsync(x => x.Id == id);


        public async Task<List<Usuario>> ObtenerUsuarios() => _context.Usuario.ToList();

    }
}
