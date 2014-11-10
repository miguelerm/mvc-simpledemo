using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcWebApplication.Models
{
    public class ModeloUsuariosCambiarClave
    {
        [Required]
        [MinLength(3)]
        [StringLength(200)]
        [DataType(DataType.Password)]
        public string ClaveActual { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(200)]
        [DataType(DataType.Password)]
        public string ClaveNueva { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(200)]
        [DataType(DataType.Password)]
        [Compare("ClaveNueva")]
        public string ClaveNuevaConfirmacion { get; set; }
    }
}