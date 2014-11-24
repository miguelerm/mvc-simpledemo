using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcWebApplication.Models;

namespace MvcWebApplication.Controllers
{
    [Authorize]
    public class ComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Compras
        public ActionResult Index()
        {
            var compras = db.Compras.Include(c => c.Proveedor).Include(x => x.Detalles);
            return View(compras.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Include(x => x.Proveedor).FirstOrDefault(x => x.Id == id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompraDetalles = db.ComprasDetalle.Include(x => x.Producto).Where(x => x.CompraId == id).ToList();
            return View(compra);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.ProveedorNit = new SelectList(db.Proveedores, "Nit", "Nombre");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,ProveedorNit")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Compras.Add(compra);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = compra.Id });
            }

            ViewBag.ProveedorNit = new SelectList(db.Proveedores, "Nit", "Nombre", compra.ProveedorNit);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.FirstOrDefault(x => x.Id == id);
            if (compra == null)
            {
                return HttpNotFound();
            }

            ViewBag.CompraDetalles = db.ComprasDetalle.Include(x => x.Producto).Where(x => x.CompraId == id).ToList();
            ViewBag.ProveedorNit = new SelectList(db.Proveedores, "Nit", "Nombre", compra.ProveedorNit);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,ProveedorNit")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompraDetalles = db.ComprasDetalle.Include(x => x.Producto).Where(x => x.CompraId == compra.Id).ToList();
            ViewBag.ProveedorNit = new SelectList(db.Proveedores, "Nit", "Nombre", compra.ProveedorNit);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompraDetalles = db.ComprasDetalle.Include(x => x.Producto).Where(x => x.CompraId == id).ToList();
            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = db.Compras.Find(id);
            db.Compras.Remove(compra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
