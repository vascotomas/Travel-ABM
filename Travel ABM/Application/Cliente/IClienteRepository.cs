using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cliente
{
    public interface IClienteRepository
    {
        Task<List<Domain.Cliente>> ObtenerClientes();
        Task<Domain.Cliente> ObtenerCliente(int clienteId);
    }
}
