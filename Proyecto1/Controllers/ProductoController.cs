using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {

                return View(db.producto.ToList());

            }
        }
        public static string NombreProveedor(int? idProveedor)
        {
            using (var db = new inventarioEntities())
            {

                return db.proveedor.Find(idProveedor).nombre;

            }


        }

        public ActionResult ListarProveedores()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.proveedor.ToList());

            }

        }
        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto producto) 
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.producto.Add(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("","Error"+ ex);
                return View();
            
            
            }
        }
        public ActionResult Edit(int id)
        { 
           using(var db=new inventarioEntities()) 
           {
                producto productoEdit = db.producto.Where(a => a.id == id).FirstOrDefault();
                return View(productoEdit);
            
           }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto productoEdit) 
        {
            try 
            {
                using (var db = new inventarioEntities())
                {
                    var oldProduct = db.producto.Find(productoEdit.id);
                    oldProduct.nombre = productoEdit.nombre;
                    oldProduct.cantidad = productoEdit.cantidad;
                    oldProduct.descripcion = productoEdit.descripcion;
                    oldProduct.percio_unitario = productoEdit.percio_unitario;
                    oldProduct.id_proveedor = productoEdit.id_proveedor;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            } catch(Exception ex) 
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();

            }

        }
        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities())
            {
                return View(db.producto.Find(id));
            
            }


        }
        public ActionResult Delete(int id)
        {
            try 
            {
                using (var db = new inventarioEntities()) 
                {
                    producto producto = db.producto.Find(id);
                    db.producto.Remove(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
            }
        
        }

        public ActionResult Reporte() 
        {
            var db = new inventarioEntities();
          
                var query = from tabProveedor in db.proveedor
                            join tabProducto in db.producto on tabProveedor.id equals tabProducto.id_proveedor
                            select new Reporte
                            {
                                nombreProveedor = tabProveedor.nombre,
                                telefonoProveedor = tabProveedor.telefono,
                                direccionProveedor = tabProveedor.direccion,
                                nombreProducto = tabProducto.nombre,
                                precioProducto = tabProducto.percio_unitario,
                            };
                return View(query);
          
        
        }
    }
}