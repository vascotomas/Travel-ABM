using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Tour
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Nombre { get; set; }
        [MaxLength(30)]
        public string Destino { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Precio { get; set; }
        public virtual ICollection<Reserva>? Tasks { get; set; } = new List<Reserva>();

    }
}