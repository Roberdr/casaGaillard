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
using CasaGaillard.Models.ViewModels;
using Microsoft.Ajax.Utilities;

namespace CasaGaillard.Areas.Mantenimiento.Controllers
{
    [Authorize]
    public class RevisionesVehiculosController : Controller
    {
        private readonly GaillardEntities db = new GaillardEntities();

        // GET: RevisionesVehiculos
        public async Task<ActionResult> Index()
        {
            var revisionesVehiculos = db.RevisionesVehiculo
                .Include(r => r.Vehiculo)
                .OrderBy(r => r.Vehiculo.MatriculaVehiculo)
                .GroupBy(r => r.Vehiculo.MatriculaVehiculo);

            var revVehiculos = from rv in revisionesVehiculos
                                  select (

                                      from r in rv
                                      group r by r.TipoRevision into z
                                      from rz in z
                                      where rz.Caducidad == z.Max(r => r.Caducidad)
                                      select new
                                      {
                                          rz.Vehiculo.MatriculaVehiculo,
                                          rz.TipoRevision,
                                          rz.Caducidad
                                      }

                                    );
            return View(await revVehiculos.ToListAsync());
        }

        // GET: RevisionesVehiculos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevisionVehiculo revisionVehiculo = await db.RevisionesVehiculo.FindAsync(id);
            if (revisionVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(revisionVehiculo);
        }

        // GET: RevisionesVehiculos/Create
        public ActionResult Create()
        {
            ViewBag.VehiculoID = new SelectList(db.Vehiculos.OrderBy(o => o.MatriculaVehiculo), "ID", "MatriculaVehiculo");
            ViewBag.TipoRevisionID = new SelectList(db.TiposRevision.OrderBy(o => o.Revision), "ID", "Revision");
            return View();
        }

        // POST: RevisionesVehiculos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,VehiculoID,TipoRevisionID,FechaRevision,Detalles,Ejecutor,Caducidad")] RevisionVehiculo revisionVehiculo)
        {
            if (ModelState.IsValid)
            {
                //revisionVehiculo.FechaRevision .AddMonths(30);
                db.RevisionesVehiculo.Add(revisionVehiculo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.VehiculoID = new SelectList(db.Vehiculos.OrderBy(o => o.MatriculaVehiculo), "ID", "MatriculaVehiculo", revisionVehiculo.VehiculoID);
            ViewBag.TipoRevisionID = new SelectList(db.TiposRevision.OrderBy(o => o.Revision), "ID", "Revision", revisionVehiculo.TipoRevisionID);
            return View(revisionVehiculo);
        }

        // GET: RevisionesVehiculos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevisionVehiculo revisionVehiculo = await db.RevisionesVehiculo.FindAsync(id);
            if (revisionVehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehiculoID = new SelectList(db.Vehiculos.OrderBy(o => o.MatriculaVehiculo), "ID", "MatriculaVehiculo", revisionVehiculo.VehiculoID);
            ViewBag.TipoRevisionID = new SelectList(db.TiposRevision.OrderBy(o => o.Revision), "ID", "Revision", revisionVehiculo.TipoRevisionID);

            return View(revisionVehiculo);
        }

        // POST: RevisionesVehiculos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,VehiculoID,TipoRevisionID,FechaRevision,Detalles,Ejecutor,Caducidad")] RevisionVehiculo revisionVehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revisionVehiculo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.VehiculoID = new SelectList(db.Vehiculos.OrderBy(o => o.MatriculaVehiculo), "ID", "MatriculaVehiculo", revisionVehiculo.VehiculoID);
            ViewBag.TipoRevisionID = new SelectList(db.TiposRevision.OrderBy(o => o.Revision), "ID", "Revision", revisionVehiculo.TipoRevisionID);

            return View(revisionVehiculo);
        }

        // GET: RevisionesVehiculos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevisionVehiculo revisionVehiculo = await db.RevisionesVehiculo.FindAsync(id);
            if (revisionVehiculo == null)
            {
                return HttpNotFound();
            }
            return View(revisionVehiculo);
        }

        // POST: RevisionesVehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RevisionVehiculo revisionVehiculo = await db.RevisionesVehiculo.FindAsync(id);
            db.RevisionesVehiculo.Remove(revisionVehiculo);
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

        public async Task<ActionResult> Proximas()
        {
            //List<Revision> rev = new List<Revision>();

            var rev1 = db.RevisionesVehiculo.Include(r => r.Vehiculo)
                  .GroupBy(s => s.Vehiculo.MatriculaVehiculo);
            return View(await rev1.ToListAsync());

        }
    }
}
