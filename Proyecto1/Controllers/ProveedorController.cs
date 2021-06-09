using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {

                return View(db.proveedor.ToList());

            }
        }
        public ActionResult create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create( proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities())
                {

                    db.proveedor.Add(proveedor);
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
                var findProveedor = db.proveedor.Find(id);
                return View(findProveedor);

            }


        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    proveedor findProveedor = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findProveedor);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();

            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(proveedor editProveedor)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    proveedor user = db.proveedor.Find(editProveedor.id);

                    user.nombre = editProveedor.nombre;
                    user.direccion = editProveedor.direccion;
                    user.telefono = editProveedor.telefono;
                    user.nombre_contacto = editProveedor.nombre_contacto;

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
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var findProveedor = db.proveedor.Find(id);
                    db.proveedor.Remove(findProveedor);
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
    }
}




