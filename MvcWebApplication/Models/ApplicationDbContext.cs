using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcWebApplication.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Compra> Compras { get; set; }

        public DbSet<CompraDetalle> ComprasDetalle { get; set; }
    }
}