using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToyStock.Models;

namespace ToyStock.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private DB db = new DB();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.Where(x=>!x.Finished).ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        public JsonResult FinishOrder(int id)
        {
            Order order = db.Orders.Find(id);
            order.Finished = true;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return Json(id);
        }
        public ActionResult Finished()
        {
            return View(db.Orders.Where(x => x.Finished).ToList());
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
