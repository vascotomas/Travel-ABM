using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Cliente
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Nombre { get; set; }
        [MaxLength(30)]
        public string Apellido { get; set; }
    }
}