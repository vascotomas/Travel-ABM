using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Reserva
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }
        public DateTime FechaReserva { get; set; }
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
    }
}
