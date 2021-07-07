using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Booking.BO;

namespace Booking.Controllers
{
    public class RestaurantsController : Controller
    {
        private BookingEntities db = new BookingEntities();

        // GET: Restaurants
        public ActionResult Index()
        {
            var restaurant = db.Restaurant.Include(r => r.Category).Include(r => r.Country).Include(r => r.KindOfFood);
            return View(restaurant.ToList());
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurant.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name");
            ViewBag.KindOfFoodId = new SelectList(db.KindOfFood, "Id", "Name");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,KindOfFoodId,CategoryId,CountryId,Picture,Enabled")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                restaurant.Id = Guid.NewGuid();
                restaurant.Rate = 5;
                restaurant.CreatedDate = DateTime.Now;

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if(file != null && file.ContentLength > 0)
                    {
                        var fileName = restaurant.Id.ToString() + "_" + Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("/Public/Restaurants/"), fileName);
                        file.SaveAs(path);

                        restaurant.Picture = fileName;
                    }
                }

                db.Restaurant.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", restaurant.CategoryId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", restaurant.CountryId);
            ViewBag.KindOfFoodId = new SelectList(db.KindOfFood, "Id", "Name", restaurant.KindOfFoodId);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurant.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", restaurant.CategoryId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", restaurant.CountryId);
            ViewBag.KindOfFoodId = new SelectList(db.KindOfFood, "Id", "Name", restaurant.KindOfFoodId);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seq,Name,KindOfFoodId,CategoryId,CountryId,Picture,Enabled,Rate,CreatedDate")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                restaurant.CreatedDate = DateTime.Now;
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", restaurant.CategoryId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", restaurant.CountryId);
            ViewBag.KindOfFoodId = new SelectList(db.KindOfFood, "Id", "Name", restaurant.KindOfFoodId);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurant.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Restaurant restaurant = db.Restaurant.Find(id);
            db.Restaurant.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Public() 
        {
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name");
            var restaurant = db.Restaurant.Include(r => r.Category).Include(r => r.Country).Include(r => r.KindOfFood);
            return View(restaurant.ToList());
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
