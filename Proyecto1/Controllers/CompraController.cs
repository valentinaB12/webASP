using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {

                return View(db.compra.ToList());

            }
        }
        public static string NombreUsuario(int? idUsuario)
        {
            using (var db = new inventarioEntities())
            {

                return db.usuario.Find(idUsuario).nombre;

            }


        }
        public static string NombreCliente(int? idCliente)
        {
            using (var db = new inventarioEntities())
            {

                return db.cliente.Find(idCliente).nombre;

            }


        }

        public ActionResult ListarUsuarios()
        { 
           using (var db=new inventarioEntities()) 
           {
                return PartialView(db.usuario.ToList());
           }
        
        }

        public ActionResult ListarClientes()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.cliente.ToList());
            }

        }
        public ActionResult Create() 
        {
            return View();
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(compra compra)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.compra.Add(compra);
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
                compra compraEdit = db.compra.Where(a => a.id == id).FirstOrDefault();
                return View(compraEdit);

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(compra compraEdit)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var oldcompra = db.compra.Find(compraEdit.id);
                    oldcompra.fecha = compraEdit.fecha;
                    oldcompra.total = compraEdit.total;
                    oldcompra.id_usuario = compraEdit.id_usuario;
                    oldcompra.id_cliente = compraEdit.id_cliente;

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
                return View(db.compra.Find(id));

            }


        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    compra compra = db.compra.Find(id);
                    db.compra.Remove(compra);
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