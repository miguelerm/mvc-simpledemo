using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebApplication.Models
{
    [Table("Proveedores")]
    public class Proveedor
    {
        [Key]
        [StringLength(50)]
        public string Nit { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(1000)]
        [Display(Name = "Dirección")]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

    }
}