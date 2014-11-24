using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWebApplication.Models
{
    [Table("Compras")]
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "NIT Proveedor")]
        public string ProveedorNit { get; set; }

        public Proveedor Proveedor { get; set; }

        public ICollection<CompraDetalle> Detalles { get; set; }

        public Compra()
        {
            Detalles = new List<CompraDetalle>();
        }
    }
}