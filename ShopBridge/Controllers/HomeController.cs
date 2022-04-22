using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ShopBridge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult GetItems()
        {
            IEnumerable<Item> items = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44392/api/");
                //HTTP GET
                var responseTask = client.GetAsync("values");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Item>>();
                    readTask.Wait();

                    items = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    items = Enumerable.Empty<Item>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(items);
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Item item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Item>("values", item);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetItems");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(item);
        }
        public ActionResult Delete()

        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("values/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("GetItems");
                }
            }

            return RedirectToAction("GetItems");
        }

        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(int id)
        {
            Item item = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64189/api/");
                //HTTP GET
                var responseTask = client.GetAsync("item?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Item>();
                    readTask.Wait();

                    item = readTask.Result;
                }
            }

            return View(item);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}