using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcWebApplication.Models
{
    public class ModeloUsuariosRegistrarse
    {
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

        [Required]
        [MinLength(3)]
        [StringLength(200)]
        [DataType(DataType.Password)]
        [Compare("Clave")]
        public string ClaveConfirmacion { get; set; }
    }
}