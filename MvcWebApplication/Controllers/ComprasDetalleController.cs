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
    public class ComprasDetalleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ComprasDetalle?compraId=5
        public ActionResult Index(int? compraId)
        {
            if (compraId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comprasDetalle = db.ComprasDetalle.Include(c => c.Producto).Where(x => x.CompraId == compraId);
            return View(comprasDetalle.ToList());
        }

        // GET: ComprasDetalle/Details?compraId=5&productoId=5
        public ActionResult Details(int? compraId, int? productoId)
        {
            if (compraId == null || productoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CompraDetalle compraDetalle = db.ComprasDetalle.Include(x => x.Producto).FirstOrDefault(x => x.CompraId == compraId && x.ProductoId == productoId);
            if (compraDetalle == null)
            {
                return HttpNotFound();
            }
            return View(compraDetalle);
        }

        // GET: ComprasDetalle/Create?compraId=5
        public ActionResult Create(int? compraId)
        {
            if (compraId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre");
            return View();
        }

        // POST: ComprasDetalle/Create?compraId=5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? compraId, [Bind(Include = "ProductoId,Cantidad")] CompraDetalle compraDetalle)
        {
            if (compraId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compraDetalle.CompraId = compraId.Value;

            if (ModelState.IsValid)
            {
                db.ComprasDetalle.Add(compraDetalle);
                db.SaveChanges();
                return RedirectToAction("Edit", "Compras", new { id = compraId });
            }

            ViewBag.CompraId = new SelectList(db.Compras, "Id", "ProveedorNit", compraDetalle.CompraId);
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre", compraDetalle.ProductoId);
            return View(compraDetalle);
        }

        // GET: ComprasDetalle/Edit?compraId=5&productoId=5
        public ActionResult Edit(int? compraId, int? productoId)
        {
            if (compraId == null || productoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraDetalle compraDetalle = db.ComprasDetalle.FirstOrDefault(x => x.CompraId == compraId && x.ProductoId == productoId);
            if (compraDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompraId = new SelectList(db.Compras, "Id", "ProveedorNit", compraDetalle.CompraId);
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre", compraDetalle.ProductoId);
            return View(compraDetalle);
        }

        // POST: ComprasDetalle/Edit?compraId=5&productoId=5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? compraId, int? productoId, [Bind(Include = "Cantidad")] CompraDetalle compraDetalle)
        {
            if (compraId == null || productoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            compraDetalle.CompraId = compraId.Value;
            compraDetalle.ProductoId = productoId.Value;

            if (ModelState.IsValid)
            {
                db.Entry(compraDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Compras", new { id = compraId });
            }
            ViewBag.CompraId = new SelectList(db.Compras, "Id", "ProveedorNit", compraDetalle.CompraId);
            ViewBag.ProductoId = new SelectList(db.Productos, "Id", "Nombre", compraDetalle.ProductoId);
            return View(compraDetalle);
        }

        // GET: ComprasDetalle/Delete?compraId=5&productoId=5
        public ActionResult Delete(int? compraId, int? productoId)
        {
            if (compraId == null || productoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraDetalle compraDetalle = db.ComprasDetalle.Include(x => x.Producto).FirstOrDefault(x => x.CompraId == compraId && x.ProductoId == productoId);
            if (compraDetalle == null)
            {
                return HttpNotFound();
            }
            return View(compraDetalle);
        }

        // POST: ComprasDetalle/Delete?compraId=5&productoId=5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? compraId, int? productoId)
        {
            if (compraId == null || productoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraDetalle compraDetalle = db.ComprasDetalle.FirstOrDefault(x => x.CompraId == compraId && x.ProductoId == productoId);
            if (compraDetalle == null)
            {
                return HttpNotFound();
            }
            db.ComprasDetalle.Remove(compraDetalle);
            db.SaveChanges();
            return RedirectToAction("Edit", "Compras", new { id = compraId });
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
