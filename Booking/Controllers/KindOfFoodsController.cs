using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Booking.BO;

namespace Booking.Controllers
{
    public class KindOfFoodsController : Controller
    {
        private BookingEntities db = new BookingEntities();

        // GET: KindOfFoods
        public ActionResult Index()
        {
            return View(db.KindOfFood.ToList());
        }

        // GET: KindOfFoods/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfFood kindOfFood = db.KindOfFood.Find(id);
            if (kindOfFood == null)
            {
                return HttpNotFound();
            }
            return View(kindOfFood);
        }

        // GET: KindOfFoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KindOfFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Enabled")] KindOfFood kindOfFood)
        {
            if (ModelState.IsValid)
            {
                kindOfFood.Id = Guid.NewGuid();
                kindOfFood.CreatedDate = DateTime.Now;

                db.KindOfFood.Add(kindOfFood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kindOfFood);
        }

        // GET: KindOfFoods/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfFood kindOfFood = db.KindOfFood.Find(id);
            if (kindOfFood == null)
            {
                return HttpNotFound();
            }
            return View(kindOfFood);
        }

        // POST: KindOfFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seq,Name,Description,Enabled,CreatedDate,CreatedByUserId")] KindOfFood kindOfFood)
        {
            if (ModelState.IsValid)
            {
                kindOfFood.CreatedDate = DateTime.Now;
                db.Entry(kindOfFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kindOfFood);
        }

        // GET: KindOfFoods/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfFood kindOfFood = db.KindOfFood.Find(id);
            if (kindOfFood == null)
            {
                return HttpNotFound();
            }
            return View(kindOfFood);
        }

        // POST: KindOfFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            KindOfFood kindOfFood = db.KindOfFood.Find(id);
            db.KindOfFood.Remove(kindOfFood);
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
