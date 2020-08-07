using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToyStock.Models;

namespace ToyStock.Controllers
{
    public class HomeController : Controller
    {
        DB db = new DB();

        /// <summary>
        /// Home Page Get Action
        /// </summary>
        /// <returns></returns>
        public ActionResult Home()
        {
            return View(db.Products.ToList());
        }
        /// <summary>
        /// This Get Action For Single Product Data View
        /// </summary>
        /// <param name="Product_ID"></param>
        /// <returns></returns>
        public ActionResult SingleProduct(int Product_ID)
        {
            Product product = db.Products.Find(Product_ID);
            return View(product);
        }
        /// <summary>
        /// This Cart Get Action
        /// </summary>
        /// <returns></returns>
        public ActionResult Cart()
        {
            List<CartData> Cart = new List<CartData>();
            if(Session["Cart"]!=null)
            {
                Cart = Session["Cart"] as List<CartData>;
            }
            return View(Cart);
        }
        /// <summary>
        /// Json Post For Add Product To Your Cart
        /// </summary>
        /// <param name="Product_ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddToCart(int Product_ID)
        {
            Product product = db.Products.Find(Product_ID);
            List<CartData> Cart = new List<CartData>();
            if (Session["Cart"]!=null)
            {
                Cart= Session["Cart"] as List<CartData>;
                Cart.Add(new CartData (product,1 ));
            }
            else
            {
                Cart = new List<CartData>();
                Cart.Add(new CartData(product,1));
                Session["Cart"] = Cart;
            }
            return Json(Cart.Count);
        }
        /// <summary>
        /// Json Post For Delete From Cart
        /// </summary>
        /// <param name="Product_ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteFromCart(int Product_ID)
        {
            if(Session["Cart"]!=null)
            {
                List<CartData> Cart = new List<CartData>();
                Cart = Session["Cart"] as List<CartData>;
                CartData product = Cart.SingleOrDefault(x=>x.Product.ID==Product_ID);
                Cart.Remove(product);
                Session["Cart"] = Cart;
            }
            return Json(Product_ID);
        }
        /// <summary>
        /// Json Post For Change Count Of Product 
        /// + Is For Add 1 And Other Sign Is For Remove 1
        /// </summary>
        /// <param name="Product_ID"></param>
        /// <param name="Sign"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeCount(int Product_ID,string Sign)
        {
            List<CartData> Cart = new List<CartData>();
            Cart = Session["Cart"] as List<CartData>;
            CartData product = Cart.SingleOrDefault(x => x.Product.ID == Product_ID);
            if(Sign == "+")
            {
                product.Count += 1;
            }
            else
            {
                product.Count -= 1;
            }
            Session["Cart"] = Cart;
            return Json(product.Count);
        }
        /// <summary>
        /// This Is Final Request For Make Order 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Order(string Name,string Phone)
        {
            if(Session["Cart"]==null)
            {
                return RedirectToAction("Home");
            }
            List<CartData> Cart = Session["Cart"] as List<CartData>;
            Order order = new Order();
            order.DateTime = DateTime.Now;
            order.Name = Name;
            order.Phone = Phone;

            double totalPrice = 0;
            foreach (var item in Cart)
            {
                order.Order_Products.Add(new Order_Products {Count=item.Count,Product_ID=item.Product.ID,Color=item.Color,Size=item.Size });                
                totalPrice += item.Count * item.Product.Price;
            }
            order.TotalPrice = totalPrice;
            db.SaveChanges();
            Session.Remove("Cart");
            return Json(true);
        }
        /// <summary>
        /// This Post Json Request For Change The Size Or Color
        /// </summary>
        /// <param name="Product_ID"></param>
        /// <param name="Size"></param>
        /// <param name="Color"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeSizeColor(int Product_ID,string Size=null,string Color=null)
        {
            List<CartData> Cart = new List<CartData>();
            Cart = Session["Cart"] as List<CartData>;
            CartData product = Cart.SingleOrDefault(x => x.Product.ID == Product_ID);
            product.Size = Size;
            product.Color = Color;
            Session["Cart"] = Cart;
            return Json(Product_ID);
        }

    }
}