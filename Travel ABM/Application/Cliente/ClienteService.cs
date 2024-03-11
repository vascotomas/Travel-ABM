using Application.GestorReservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cliente
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<Domain.Cliente> ObtenerCliente(int clienteId) => await _clienteRepository.ObtenerCliente(clienteId);

        public async Task<List<Domain.Cliente>> ObtenerClientes()=> await _clienteRepository.ObtenerClientes();
    }
}
