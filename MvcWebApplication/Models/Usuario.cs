using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcWebApplication.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(20)]
        public string Login { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(200)]
        public string Correo { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(200)]
        [DataType(DataType.Password)]
        public string Clave { get; set; }

        public ICollection<Rol> Roles { get; set; }

        public Usuario()
        {
            Roles = new HashSet<Rol>();
        }
    }
}