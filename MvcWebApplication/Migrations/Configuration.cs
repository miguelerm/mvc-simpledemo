namespace MvcWebApplication.Migrations
{
    using MvcWebApplication.Controllers;
    using MvcWebApplication.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcWebApplication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MvcWebApplication.Models.ApplicationDbContext";
        }

        protected override void Seed(MvcWebApplication.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var admins = AgregarRolAdministradores(context);
            var admin = AregarUsuarioAdministrador(context);

            AgregarRolUsuarios(context);
            
            AsignarRolAdminsAlAdmin(context, admin, admins);

        }

        private void AsignarRolAdminsAlAdmin(ApplicationDbContext context, Usuario admin, Rol admins)
        {
            // solo se agrega el rol si el usuario admin está recien agregado.
            if (context.Entry(admin).State != EntityState.Added)
                return;

            // se omite si el usuairo ya tiene el rol
            if (admin.Roles.Any(x => x.Nombre == admins.Nombre))
                return;

            admin.Roles.Add(admins);
        }

        private static Usuario AregarUsuarioAdministrador(MvcWebApplication.Models.ApplicationDbContext context)
        {
            var admin = context.Usuarios.Include(x => x.Roles).FirstOrDefault(x => x.Login == "admin");

            if (admin == null)
            {
                admin = new Usuario
                {
                    Login = "admin",
                    Nombre = "Administrador del sistema",
                    Correo = "admin@demo",
                    FechaCreacion = DateTime.Now,
                    Clave = UsuariosController.Encriptar("admin", "admin")
                };

                context.Usuarios.Add(admin);
            }

            return admin;
        }

        private static void AgregarRolUsuarios(MvcWebApplication.Models.ApplicationDbContext context)
        {
            var users = context.Roles.FirstOrDefault(x => x.Nombre == "Muggles");

            if (users == null)
            {
                users = new Rol
                {
                    Nombre = "Muggles",
                    Descripcion = "Usuarios normales del sistema."
                };

                context.Roles.Add(users);
            }
        }

        private static Rol AgregarRolAdministradores(MvcWebApplication.Models.ApplicationDbContext context)
        {
            var admins = context.Roles.FirstOrDefault(x => x.Nombre == "Almighties");

            if (admins == null)
            {
                admins = new Rol
                {
                    Nombre = "Almighties",
                    Descripcion = "Administradores del sistema."
                };

                context.Roles.Add(admins);
            }
            return admins;
        }
    }
}
