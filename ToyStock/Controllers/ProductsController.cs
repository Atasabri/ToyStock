using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToyStock.Models;
using System.IO;

namespace ToyStock.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private DB db = new DB();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,Description,Code")] Product product,List<HttpPostedFileBase> Photos)
        {
            if (ModelState.IsValid&&Photos[0]!=null)
            {
                db.Products.Add(product);               
                db.SaveChanges();
                foreach (var item in Photos)
                {
                    item.SaveAs(Server.MapPath("~/Uploads/" + product.ID + "IMG" + item.FileName));
                }
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Description,Code")] Product product,List<HttpPostedFileBase> Photos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                if(Photos[0]!=null)
                {
                    foreach (var item in Photos)
                    {
                        item.SaveAs(Server.MapPath("~/Uploads/" + product.ID + "IMG" + item.FileName));
                    }
                }
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            string[]Paths= Directory.GetFiles(Server.MapPath("~/Uploads"),id+"IMG*");
            foreach (var item in Paths)
            {
                FileInfo F = new FileInfo(item);
                if(F.Exists)
                {
                    F.Delete();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeletePhoto(string Path)
        {
            FileInfo F = new FileInfo(Path);
            if(F.Exists)
            {
                F.Delete();
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
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
