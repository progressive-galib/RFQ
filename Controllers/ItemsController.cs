using Microsoft.AspNetCore.Mvc;
using RFQ.Models;

namespace RFQ.Controllers
{
    public class ItemsController : Controller
    {
        // GET: ItemsController
        public ActionResult Overview()
        {
            Item item= new Item(){Name ="Umapyoi"};
            return View(item);
        }

        public IActionResult Urlparam(int id){
            return Content (id.ToString());
        }

    }
}
