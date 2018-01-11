using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using IA.Models;
namespace IA.Controllers
{
    public class AdminController : Controller
    {
        Database2Entities3 db = new Database2Entities3();
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult manageusers()
        {
            return View(db.users.ToList());
        }

        public ActionResult details()
        {

            return View(db.users.Find(1));
        }

        public ActionResult Deleteuser(int id)
        {
            user p = db.users.Find(id);
            db.users.Remove(p);
            db.SaveChanges();
            return RedirectToAction("manageusers");
        }

        public ActionResult Adduser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Adduser(user u)
        {
            db.users.Add(u);
            db.SaveChanges();
            return RedirectToAction("manageusers");
        }

        public ActionResult manage_pending_projects()
        {

            return View(db.projects.Where(u => u.assigend_state_id == 3 && u.posting_state_id == 1).ToList());
            
        }

        public ActionResult projectapprove(int id)
        {
            project result = (from p in db.projects
                           where p.Id == id
                           select p).SingleOrDefault();

            result.posting_state_id = 2;

            db.SaveChanges();

            return RedirectToAction("manage_pending_projects");
        }

        public ActionResult projectdelete(int id)
        {
            project p = db.projects.Find(id);
            db.projects.Remove(p);
            db.SaveChanges();
            return RedirectToAction("manage_pending_projects");
        }
        public ActionResult projecthomedelete(int id)
        {
            project p = db.projects.Find(id);
            db.projects.Remove(p);
            db.SaveChanges();
            return RedirectToAction("manage_home_page");
        }

        public ActionResult manage_home_page()
        {
            return View(db.projects.Where(u => u.assigend_state_id == 3 && u.posting_state_id == 2).ToList());
        }

        [HttpPost]
       public ActionResult update_home_project(int id)
        {
            project result = (from p in db.projects
                              where p.Id == id
                              select p).SingleOrDefault();

                       db.SaveChanges();

            return RedirectToAction("manage_pending_projects");
        }



        public ActionResult Projectdetails(int? id)
        {
            if (id == null)
 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
            }
            else
                return View(db.projects.Find(id));
            
        }
        public ActionResult Statistical()
        {
            return View();
        }
    }
}