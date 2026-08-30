using System.ComponentModel.DataAnnotations;

namespace WEB_APP_FINAL.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required(ErrorMessage = "A product must have a price")]
        public float Price { get; set; }
        [Required(ErrorMessage = "A product must have a stock")]
        public int Stock { get; set; }
        public List<Tarea>? Tareas { get; set; }
    }
}
