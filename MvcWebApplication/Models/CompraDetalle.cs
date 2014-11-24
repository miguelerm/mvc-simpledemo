using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebApplication.Models
{
    [Table("ComprasDetalle")]
    public class CompraDetalle
    {
        [Key]
        [Column(Order = 0)]
        public int CompraId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ProductoId { get; set; }

        public Compra Compra { get; set; }

        public Producto Producto { get; set; }

        public decimal Cantidad { get; set; }

    }
}