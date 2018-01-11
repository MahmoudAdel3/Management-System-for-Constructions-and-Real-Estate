using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA.Models;

namespace IA.Controllers
{
    public class UserController : Controller
    {
        Database2Entities3 db = new Database2Entities3();
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult login(user user)
        {
            user myUser = db.users.FirstOrDefault(u => u.email.Equals(user.email) && u.password.Equals(user.password));

            if (myUser != null)
            {
                //TempData["ID"] = myUser.id;
                Session["ID"] = myUser.id;
                Session["phone"] = myUser.phone_number;
                Session["name"] = myUser.fname + " " + myUser.lname;
                Session["usertype"] = myUser.type_id;
                Session["email"] = myUser.email;
                //Session["jobdesc"] = myUser.user_type.name;
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.message = "User name or password is not Valid";
                return RedirectToAction("Index", "Home");
            }
 
        }

        public ActionResult logout()
        {
            Session.Abandon();//DESTROY SESSION
            return RedirectToAction("index","Home");
        }


        public ActionResult register()
        {
            return View();
        }



        [HttpPost]
        public ActionResult register(user u)
        {
            if(u!=null)
            {
                Session["ID"] = u.id;
                Session["name"] = u.fname + " " + u.lname;
                Session["usertype"] = u.user_type;
                Session["email"] = u.email;
                db.users.Add(u);
                db.SaveChanges();
                login(u);
                return RedirectToAction("index","Home");
            }
            else
            {
                ViewBag.message = "please fill all filds";
                return View();
            }
            
        }
        
	}



}