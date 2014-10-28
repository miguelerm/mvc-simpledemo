using MvcWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcWebApplication.Controllers
{
    public class UsuariosController : Controller
    {
        // se abre la conexion a la base de datos.
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Ingresar()
        {
            return View();
        }

        public ActionResult Ingresar(ModeloUsuariosIngresar modelo)
        {
            if (ModelState.IsValid)
            {

                // Se busca el usuario con el login indicado.
                var usuario = db.Usuarios.FirstOrDefault(x => x.Login == modelo.Login.ToLower());
                if (usuario == null)
                {
                    ModelState.AddModelError("", "Usuario o clave incorrectas");
                    return View(modelo);
                }


                // Se comparan las claves (ambas encriptadas)
                if (usuario.Clave != Encriptar(modelo.Login, modelo.Clave))
                {
                    ModelState.AddModelError("", "Usuario o clave incorrectas");
                    return View(modelo);
                }

                // Se  crea la cookie de autenticacion.
                FormsAuthentication.SetAuthCookie(usuario.Login, modelo.Recordarme);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(modelo);
            }
        }


        public ActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrarse(ModeloUsuariosRegistrarse modelo)
        {
            if (ModelState.IsValid)
            {
                var usuario = db.Usuarios.FirstOrDefault(x => x.Login == modelo.Login.ToLower());

                if (usuario != null)
                {
                    ModelState.AddModelError("Login", "Ya existe un usuario registrado con el mismo login");
                    return View(modelo);
                }

                usuario = db.Usuarios.FirstOrDefault(x => x.Correo == modelo.Correo.ToLower());

                if (usuario != null)
                {
                    ModelState.AddModelError("Correo", "Ya existe un usuario registrado con el mismo correo");
                    return View(modelo);
                }

                usuario = new Usuario
                {
                    FechaCreacion = DateTime.Now,
                    Login = modelo.Login,
                    Nombre = modelo.Nombre,
                    Correo = modelo.Correo,
                    Clave = Encriptar(modelo.Login, modelo.Clave)
                };

                db.Usuarios.Add(usuario);
                db.SaveChanges();

                return RedirectToAction("RegistroFinalizado");
            }
            else
            {
                return View(modelo);
            }
        }

        public ActionResult RegistroFinalizado()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            // se cierra la conexion a la base de datos.
            db.Dispose();
            base.Dispose(disposing);
        }

        private string Encriptar(string login, string clave)
        {
            // Se concatena el login + clave + login para que
            // si dos usuarios tienen claves iguales, no se sepa
            // ya que el md5 será distinto por contener el login.
            var texto = string.Format("{0}|{1}|{0}", login, clave);
            return CalcularMd5(texto);
        }

        private string CalcularMd5(string texto)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash. 
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                // Create a new Stringbuilder to collect the bytes 
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data  
                // and format each one as a hexadecimal string. 
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string. 
                return sBuilder.ToString();
            }
        }

    }
}