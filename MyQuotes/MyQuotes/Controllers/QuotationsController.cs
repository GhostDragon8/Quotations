using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyQuotes.Models;

namespace MyQuotes.Controllers
{
    public class QuotationsController : Controller
    {
        private MyQuotesContext db = new MyQuotesContext();

        // GET: Quotations
        public ActionResult Index(string searchTerms)
        {
            ViewBag.Search = false;
            var all = from a in db.Quotations select a;

            if (!(string.IsNullOrEmpty(searchTerms)))
            {
                ViewBag.Search = true;
                all = all.Where(a => a.Quote.Contains(searchTerms) || a.Author.Contains(searchTerms) || a.Category.Name.Contains(searchTerms));

            }

            var quotations = db.Quotations.Include(q => q.Category);
            return View(all);
        }

        // GET: Quotations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotations quotations = db.Quotations.Find(id);
            if (quotations == null)
            {
                return HttpNotFound();
            }
            return View(quotations);
        }

        // GET: Quotations/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Quotations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Author,Quote,DateAdded,CategoryID")] Quotations quotations, string input)
        {
            quotations.DateAdded = DateTime.Now;

            //retreives current category names in database
            var catNames = from c in db.Categories select c.Name;

            //if textbox 'input' is empty/null and db does not contain input:
            if (!string.IsNullOrEmpty(input) && (!catNames.Contains(input)))
            {
                ModelState.Remove("CategoryID");
                //make new category object
                Category newCat = new Category();

                //set object.Name equal to user input
                newCat.Name = input;

                //sets new object ID to the quotation model
                quotations.CategoryID = db.Categories.Add(newCat).ID;
            }
            if (ModelState.IsValid)
            {
                db.Quotations.Add(quotations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

                ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", quotations.CategoryID);
            return View(quotations);
        }
        // GET: Quotations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotations quotations = db.Quotations.Find(id);
            if (quotations == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", quotations.CategoryID);
            return View(quotations);
        }

        // POST: Quotations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Author,Quote,DateAdded,CategoryID")] Quotations quotations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quotations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", quotations.CategoryID);
            return View(quotations);
        }

        // GET: Quotations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotations quotations = db.Quotations.Find(id);
            if (quotations == null)
            {
                return HttpNotFound();
            }
            return View(quotations);
        }

        // POST: Quotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quotations quotations = db.Quotations.Find(id);
            db.Quotations.Remove(quotations);
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
