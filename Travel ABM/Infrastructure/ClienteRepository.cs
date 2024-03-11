using Application.Cliente;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly TravelContext _context;

        public ClienteRepository(TravelContext context)
        {
            _context = context;
        }

        public async Task<Cliente> ObtenerCliente(int clienteId) => await _context.Cliente.FindAsync(clienteId);


        public async Task<List<Cliente>> ObtenerClientes()=> await _context.Cliente.ToListAsync();
    }
}
