﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasaGaillard.Models;

namespace CasaGaillard.Controllers
{
    public class EntidadesController : Controller
    {
        private readonly GaillardEntities db = new GaillardEntities();

        // GET: Entidades
        public async Task<ActionResult> Index()
        {
            var entidads = db.Entidads.Include(e => e.Direccion);
            return View(await entidads.ToListAsync());
        }

        // GET: Entidades/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entidad entidad = await db.Entidads.FindAsync(id);
            if (entidad == null)
            {
                return HttpNotFound();
            }
            return View(entidad);
        }

        // GET: Entidades/Create
        public ActionResult Create()
        {
            ViewBag.DireccionID = new SelectList(from d in db.Direcciones select new { d.ID, nombreVia = d.NombreVia + ", " + d.Numero + " (" + d.Poblacion.NombrePoblacion + ")" }, "ID", "nombreVia");
            return View();
        }

        // POST: Entidades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NombreEntidad,CIF,DireccionID")] Entidad entidad)
        {
            if (ModelState.IsValid)
            {
                db.Entidads.Add(entidad);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DireccionID = new SelectList(from d in db.Direcciones select new {d.ID, nombreVia = d.NombreVia + ", " + d.Numero + " (" + d.Poblacion.NombrePoblacion + ")" }, "ID", "nombreVia" , entidad.DireccionID);
            //ViewBag.DireccionID = new SelectList(db.Direcciones, "ID", "NombreVia", entidad.DireccionID);
            return View(entidad);
        }

        // GET: Entidades/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entidad entidad = await db.Entidads.FindAsync(id);
            if (entidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.DireccionID = new SelectList(from d in db.Direcciones select new { d.ID, nombreVia = d.NombreVia + ", " + d.Numero + " (" + d.Poblacion.NombrePoblacion +  ")" }, "ID", "nombreVia", entidad.DireccionID); 
            return View(entidad);
        }

        // POST: Entidades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NombreEntidad,CIF,DireccionID")] Entidad entidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entidad).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DireccionID = new SelectList(from d in db.Direcciones.ToList() select new { d.ID, nombreVia = d.NombreVia + ", " + d.Numero + " (" + d.Poblacion.NombrePoblacion + ")" }, "ID", "nombreVia", entidad.DireccionID); 
            return View(entidad);
        }

        // GET: Entidades/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entidad entidad = await db.Entidads.FindAsync(id);
            if (entidad == null)
            {
                return HttpNotFound();
            }
            return View(entidad);
        }

        // POST: Entidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Entidad entidad = await db.Entidads.FindAsync(id);
            db.Entidads.Remove(entidad);
            await db.SaveChangesAsync();
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
