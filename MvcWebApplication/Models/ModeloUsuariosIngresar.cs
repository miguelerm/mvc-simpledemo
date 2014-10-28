using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcWebApplication.Models
{
    public class ModeloUsuariosIngresar
    {
        [Required]
        [StringLength(20)]
        public string Login { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(200)]
        [DataType(DataType.Password)]
        public string Clave { get; set; }

        public bool Recordarme { get; set; }
    }
}