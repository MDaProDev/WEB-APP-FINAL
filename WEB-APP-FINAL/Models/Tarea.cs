using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_APP_FINAL.Models
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "A Task must contain a description")]
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now; //This is really imprtant to make this kinda relation work
        public bool Done { get; set; } = false;
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; } // The interrogation too.
    }
}
