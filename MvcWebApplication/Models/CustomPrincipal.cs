using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MvcWebApplication.Models
{
    public class CustomPrincipal : IPrincipal
    {
        private readonly ApplicationDbContext db;

        public CustomPrincipal(string name)
        {
            db = new ApplicationDbContext();
            this.Identity = new GenericIdentity(name);
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            if (!this.Identity.IsAuthenticated)
                return false;

            var username = this.Identity.Name.ToLower();
            role = role.ToLower();
            return db.Usuarios.Where(x => x.Login.ToLower() == username && x.Roles.Any(y => y.Nombre.ToLower() == role)).Any();
        }
    }
}