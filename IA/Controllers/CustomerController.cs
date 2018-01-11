using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA.Models;


namespace Test.Controllers
{
	public class CustomerController : Controller
	{
        Database2Entities3 db = new Database2Entities3();

		//
		// GET: /Customer/
		public ActionResult Index()
		{
			return View();
		}



		public ActionResult CreateProject()
		{
			return View();
		}

        


		[HttpPost]
		public ActionResult CreateProject(project project)
		{

			project.assigend_state_id = 3;
			project.posting_state_id = 1;
            int id = Convert.ToInt32(Session["ID"]);
            project.customer_id = id;
            project.creation_date = DateTime.Now;
			db.projects.Add(project);
		

			db.SaveChanges();
			return  RedirectToAction("GetProjects");
		}
		public ActionResult GetProjects()
		{
            int id = Convert.ToInt32(Session["ID"]);
            return View(db.projects.Where(u =>  u.customer_id == id).ToList());
		}

		public ActionResult Edit(int? id)
		{

			return View(db.projects.Find(id));
		}
		[HttpPost]
		public ActionResult Edit(project project)
		{
			project result = (from p in db.projects
							 where p.Id == project.Id
							 select p).SingleOrDefault();
			result.description=project.description;
			result.duration=project.duration;
			result.title=project.title;

			db.SaveChanges();
			return RedirectToAction("GetProjects");
		}


		public ActionResult Remove(int ?id)
		{

			project project = db.projects.Find(id);

			if (project.assigend_state_id == 4)
			{
				return Content("Error !!, its already assigned to user !!");
			}
			else
			{
			
		   

				db.projects.Remove(project);
				db.SaveChanges();
				return RedirectToAction("GetProjects");

			}

			

		}



        public ActionResult Details(int? id)
        {

            return View(db.projects.Find(id));
        }

        public ActionResult get_projectmanger_request()
        {
            int id = Convert.ToInt32(Session["ID"]); 
            return View(db.requests.Where(u => u.reciever_id== id&& u.state_id==1).ToList());
        }

        public ActionResult Assign_pm(int id)
        {
            request result = (from p in db.requests
                              where p.Id == id
                              select p).SingleOrDefault();
            result.state_id = 4;
            mangaed_projects mp = new mangaed_projects();
            mp.pm_id = result.sender_id;
            mp.project_id = result.project_id;
            mp.state_id = 6;
            mp.start_date = DateTime.Now;
            mp.deliver_date = DateTime.Now;
            mp.cost = 5000;
            db.mangaed_projects.Add(mp);
            db.SaveChanges();
            return RedirectToAction("get_projectmanger_request");
        }
	}
}