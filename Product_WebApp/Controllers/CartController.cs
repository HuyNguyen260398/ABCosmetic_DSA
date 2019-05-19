using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Product_WebApp.Models;

namespace Product_WebApp.Controllers
{
    public class CartController : Controller
    {
        private string url_orderdetails = "http://localhost:52517/api/OrderDetails/";
        private string url_order = "http://localhost:50091/api/Order/";
        private string url_product = "http://localhost:63380/api/Product/";
        HttpClient httpClient = new HttpClient();

        #region Create Cart

        // Get Cart
        public List<Cart> getCart()
        {
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart == null)
            {
                listCart = new List<Cart>();
                Session["Cart"] = listCart;
            }
            return listCart;
        }

        // Add Cart
        public ActionResult AddCart(int proId, string strURL)
        {
            var model = JsonConvert.DeserializeObjectAsync<Product>(httpClient.GetStringAsync(url_product + proId).Result).Result;

            if (model == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listCart = getCart();
            Cart item = listCart.Find(c => c.ProductId == proId);
            if (item == null)
            {
                item = new Cart(proId);
                listCart.Add(item);
                return Redirect(strURL);
            }
            else
            {
                item.Quantity++;
                return Redirect(strURL);
            }
        }

        // Update Cart
        public ActionResult UpdateCart(int proId, FormCollection f)
        {
            var product = JsonConvert.DeserializeObjectAsync<Product>(httpClient.GetStringAsync(url_product + proId).Result).Result;
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listCart = getCart();
            Cart item = listCart.SingleOrDefault(i => i.ProductId == proId);
            if (item != null)
            {
                item.Quantity = int.Parse(f["txtQuantity"].ToString());
            }
            return RedirectToAction("Cart");
        }

        // Delete Cart
        public ActionResult DeleteCart(int proId)
        {
            var product = JsonConvert.DeserializeObjectAsync<Product>(httpClient.GetStringAsync(url_product + proId).Result).Result;
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listCart = getCart();
            Cart item = listCart.SingleOrDefault(i => i.ProductId == proId);
            if (item != null)
            {
                listCart.RemoveAll(c => c.ProductId == proId);
            }
            return RedirectToAction("Cart");
        }

        // Cart Action
        public ActionResult Cart()
        {
            List<Cart> listCart = getCart();
            Session["Payment"] = TotalMoney();
            return View(listCart);
        }

        // Get Total Amount
        private int TotalAmount()
        {
            int totalAmount = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart != null)
            {
                totalAmount = listCart.Sum(q => q.Quantity);
            }
            return totalAmount;
        }

        // Get Total Money
        private double TotalMoney()
        {
            double totalMoney = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart != null)
            {
                totalMoney = listCart.Sum(q => q.Subtotal);
            }
            return totalMoney;
        }

        // Create Cart Partial
        public ActionResult CartPartial()
        {
            if (TotalAmount() == 0)
            {
                return PartialView();
            }
            ViewBag.TotalAmount = TotalAmount();
            ViewBag.TotalMoney = TotalMoney();
            return PartialView();
        }

        // Update Quantity
        public ActionResult UpdateQuantity()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart> listCart = getCart();
            return View(listCart);
        }
        #endregion

        #region Create Order
        public ActionResult OrderAndPurchase()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrderAndPurchase(Order model)
        {
            List<Cart> listCart = getCart();
            if (ModelState.IsValid)
            {
                try
                {
                    var res = httpClient.PostAsJsonAsync<Order>(url_order, model).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "success");
                    }
                    else
                    {
                        ModelState.AddModelError("", "failed");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }

                var orderList = JsonConvert.DeserializeObjectAsync<IEnumerable<Order>>(httpClient.GetStringAsync(url_order).Result).Result;
                var orderid = orderList.Last();

                foreach (var item in listCart)
                {
                    OrderDetails orderDetail = new OrderDetails
                    {
                        Order_ID = orderid.ID,
                        Product_ID = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.ProductPrice,
                        Subtotal = item.Subtotal
                    };
                    try
                    {
                        var details = httpClient.PostAsJsonAsync<OrderDetails>(url_orderdetails, orderDetail).Result;

                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                }
                listCart.Clear();
            }
            return View(model);
        }

        #endregion
    }
}