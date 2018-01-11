using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA.Models;


namespace IA.Controllers
{
    public class HomeController : Controller
    {
        Database2Entities3 db = new Database2Entities3();
        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult projects()
        {
            return PartialView("projects",db.projects.ToList());
        }

        [HttpPost]
        public JsonResult List()
        {
            var states = db.user_type.Where(c => c.id != 1).ToList();
            List<SelectListItem> listates = new List<SelectListItem>();

            listates.Add(new SelectListItem { Text = "Select Type", Value = "0" });
            if (states != null)
            {
                foreach (var x in states)
                {
                    listates.Add(new SelectListItem { Text = x.name, Value = x.id.ToString() });

                }



            }


            return this.Json(new SelectList(listates, "Value", "Text", JsonRequestBehavior.AllowGet));
        }  
        
	}
}