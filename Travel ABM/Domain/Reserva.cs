using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Reserva
    {        
        public int Id { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        [JsonIgnore]
        public virtual Cliente? Cliente { get; set; }
        public DateTime FechaReserva { get; set; }
        [ForeignKey("Tour")]
        public int IdTour { get; set; }
        [JsonIgnore]
        public virtual Tour? Tour { get; set; }
    }
}
