using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class Producto_CompraController : Controller
    {
        // GET: Producto_Compra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {

                return View(db.producto_compra.ToList());

            }
        }

        public static string NombreProducto(int?idProducto)
        {
            using (var db = new inventarioEntities())
            {

                return db.producto.Find(idProducto).nombre;

            }


        }

        public ActionResult ListarCompras()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.compra.ToList());
            }

        }
        public ActionResult ListarProductos()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.producto.ToList());
            }

        }
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_compra producto_Compra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.producto_compra.Add(producto_Compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();


            }
        }
        public ActionResult Edit(int id)
        {
            using (var db = new inventarioEntities())
            {
                compra producto_compraEdit = db.compra.Where(a => a.id == id).FirstOrDefault();
                return View(producto_compraEdit);

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto_compra producto_compraEdit)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var oldProducto_compra = db.producto_compra.Find(producto_compraEdit);
                    oldProducto_compra.id_compra = producto_compraEdit.id_compra;
                    oldProducto_compra.id_producto = producto_compraEdit.id_producto;
                    oldProducto_compra.cantidad = producto_compraEdit.cantidad;


                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();

            }

        }
        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities())
            {
                return View(db.producto_compra.Find(id));

            }


        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto_compra producto_Compra = db.producto_compra.Find(id);
                    db.producto_compra.Remove(producto_Compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
            }

        }
}   }