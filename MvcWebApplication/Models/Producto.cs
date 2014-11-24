using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebApplication.Models
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name="Descripción")]
        public string Descripcion { get; set; }

        public decimal Precio { get; set; }
    }
}