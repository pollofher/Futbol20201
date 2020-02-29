using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EquipoController : Controller
    {
        private OnlineShopEntities db = new OnlineShopEntities();
        private SubirArchivo arc = new SubirArchivo();

        
        public ActionResult Index()
        {

            try
            {
                ViewBag.sms = TempData["sms"].ToString();

            }
            catch { }
            try
            {

                ViewBag.smsok = TempData["smsok"].ToString();
            }
            catch { }

            List<Equipo> OrderAndCustomerList = db.Equipo.ToList();
            return View(OrderAndCustomerList);
        }


        // POST: Equipo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "EquipoId,nombre,colores,fundacion,Id,escudo")] Equipo equipo)
        {
            HttpPostedFileBase escudo = Request.Files[0];
            string ruta = Server.MapPath("~/ImgEquipo/");
            ruta += escudo.FileName;
            escudo.SaveAs(ruta);
            equipo.escudo = escudo.FileName;

            if (ModelState.IsValid)
            {
                equipo.EquipoId = Guid.NewGuid();
                db.Equipo.Add(equipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipo);
        }

        public ActionResult SaveOrder([Bind(Include ="escudo")]Equipo equipo,string nombre, String colores, Jugador[] order,Jugador jugador,HttpPostedFileBase foto_jugador)
        {
            


            string result = "Error! Order Is Not Complete!";
            if (nombre != null && colores != null && order != null && foto_jugador != null)
            {
                HttpPostedFileBase escudo = Request.Files[0];
                string ruta = Server.MapPath("~/ImgEquipo/");
                ruta += escudo.FileName;
                escudo.SaveAs(ruta);
                equipo.escudo = escudo.FileName;
            }
            //{
               
            //    string ruta1 = Server.MapPath("~/ImgJugador/");
            //    ruta1 += foto_jugador.FileName;
            //    foto_jugador.SaveAs(ruta1);
            //    jugador.foto_jugador = foto_jugador.FileName;
            //}
            {
                var equipoId = Guid.NewGuid();
                Equipo model = new Equipo();
                model.EquipoId = equipoId;
                model.nombre = nombre;
                model.colores = colores;
                model.fundacion = DateTime.Now;
                model.escudo = model.escudo;
                db.Equipo.Add(model);

                foreach (var item in order)
                {
                   
                    var jugadorId = Guid.NewGuid();
                    Jugador O = new Jugador();
                    O.JugadorId = jugadorId;
                    O.cedula = item.cedula;
                    O.nombres = item.nombres;
                    O.apellidos = item.apellidos;
                    O.carnet = item.carnet;
                    O.profesion = item.profesion;
                    O.intruccion = item.intruccion;
                    O.estadocivil = item.estadocivil;
                    O.foto_jugador = item.foto_jugador;
                    O.nacimiento = item.nacimiento;
                    O.afiliacion = item.afiliacion;
                    O.EquipoId = equipoId;
                    db.Jugador.Add(O);
                }
                db.SaveChanges();
                result = "Terminado! confirme si sus Jugadores estan completos!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditOrder(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Jugador order = db.Jugador.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipoId = new SelectList(db.Equipo, "EquipoId", "nombre", order.EquipoId);
            return View(order);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder([Bind(Include = "JugadorId,cedula,nombres,apellidos,carnet,profesion,intruccion,estadocivil,foto_jugador,EquipoId")] Jugador order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipoId = new SelectList(db.Equipo, "EquipoId", "nombre", order.EquipoId);
            return View(order);
        }



        public ActionResult EditCustomer(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Equipo cus = db.Equipo.Find(id);
            if (cus == null)
            {
                return HttpNotFound();
            }

            return View(cus);

        }


        [HttpPost]
        
        public ActionResult EditCustomer([Bind(Include = "EquipoId,nombre,colores,fundacion")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipo);
        }

        //Eliminar JSON

        //public JsonResult Delete(Guid? id)
        //{
        //    bool result = false;
        //    Equipo s = db.Equipo.Where(x => x.EquipoId == id).SingleOrDefault();
        //    if (s != null)
        //    {

        //        db.SaveChanges();
        //        result = true;
        //    }

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}


        //Eliminar
        // GET: Campeonatoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Equipo cus = db.Equipo.Find(id);
                db.Equipo.Remove(cus);
                db.SaveChanges();
                TempData["smsok"] = "El dato se elimino correctamente";
                ViewBag.smsok = TempData["smsok"];
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["sms"] = "No se puede eliminar el Equipo, porque primero se deben eliminar los jugadores";
                ViewBag.sms = TempData["sms"];
                return RedirectToAction("Index");
            }

        }


        //// POST: Campeonatoes/Delete/5
        //[HttpPost, ActionName("Delete")]

        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Equipo cus = db.Equipo.Find(id);
        //    db.Equipo.Remove(cus);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //Eliminar
        // GET: Campeonatoes/Delete/5
        public ActionResult DeleteJugador(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Jugador order = db.Jugador.Find(id);
                db.Jugador.Remove(order);
                db.SaveChanges();
                TempData["smsok"] = "El dato se elimino correctamente";
                ViewBag.smsok = TempData["smsok"];
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["sms"] = "No se puede eliminar los registros";
                ViewBag.sms = TempData["sms"];
                return RedirectToAction("Index");
            }

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


  