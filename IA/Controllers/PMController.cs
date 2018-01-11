using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA.Models;

namespace Test.Controllers
{
    public class PMController : Controller
    {

        Database2Entities3 db = new Database2Entities3();
        //
        // GET: /PM/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult getCutomerProjects()
        {
           
            //return Content("user"+x);
            return View(db.projects.Where(u => u.assigend_state_id == 3 && u.posting_state_id == 2).ToList());
        }

        public ActionResult getPmProjects()
        {
              int pm_id = Convert.ToInt32(Session["ID"]);
            List<project> list = new List<project>();

            var projects = db.mangaed_projects.Where(s => s.pm_id == pm_id);
            if(projects.Count() !=0)
            {
                foreach(var x in projects)
                {
                    list.Add(db.projects.Find(x.project_id));
                }
            }
            return View(list);

        }

        public ActionResult Details(int? id)
        {
            //var x = Session["ID"];
            //return Content("user"+x);
            return View(db.projects.Find(id));
        }

        public ActionResult Manage(int? id)
        {
            Session["project_id"] =id;

            return View();
        }

        [HttpPost]
        public ActionResult Manage(mangaed_projects ma)
        {

            int project_id = Convert.ToInt32(Session["project_id"]);
            int pm_id = Convert.ToInt32(Session["ID"]);

            ma.project_id =project_id;
            ma.pm_id = pm_id;
            ma.state_id = 6;// om progress
            

          
            db.mangaed_projects.Add(ma);


            db.SaveChanges();
            return RedirectToAction("getCutomerProjects");
        }

        public ActionResult getTeamLeaders()
        {

            return View(db.users.Where(u => u.type_id == 5).ToList());
        }


        public ActionResult getJuniorEngineers()
        {
            //junior engineer
            return View(db.users.Where(u => u.type_id == 3).ToList());
        }
        public ActionResult SendRequestToTeamLeader(int? id)
        {
            Session["leaderId"] = id;
 

            return View();
        }

        [HttpPost]
        public ActionResult SendRequestToTeamLeader(request req)
        {
          
           int pm_id = Convert.ToInt32(Session["ID"]);
           int tl_id = Convert.ToInt32(Session["leaderId"]);

           req.state_id = 1;
           req.sender_id = pm_id;
           req.reciever_id =tl_id;
      
            db.requests.Add(req);
            db.SaveChanges();

      
            return RedirectToAction("getPmProjects");
        }
        //SendRequestToJuniorEngineer


        public ActionResult SendRequestToJuniorEngineer(int? id)
        {
            Session["developerId"] = id;


            return View();
        }

        [HttpPost]
        public ActionResult SendRequestToJuniorEngineer(request req)
        {

            int pm_id = Convert.ToInt32(Session["ID"]);
            int tl_id = Convert.ToInt32(Session["developerId"]);
            req.state_id = 1;
            req.sender_id = pm_id;
            req.reciever_id = tl_id;

            db.requests.Add(req);
            db.SaveChanges();


            return RedirectToAction("getPmProjects");
        }


        public ActionResult deliverProject(int? id)
        {

            mangaed_projects result = (from p in db.mangaed_projects
                              where p.project_id == id
                              select p).FirstOrDefault();


            int result_com = DateTime.Compare(result.deliver_date, DateTime.Now);
            

            if (result_com < 0){
                result.deliver_date = DateTime.Now;
                result.state_id = 7;
                db.SaveChanges();
                
        }
            
            else{
                return Content("Error");

             }
            
          
           
           return RedirectToAction("getPmProjects");
           
        }


        public ActionResult getDeliveredProjects()
        {

            int pm_id = Convert.ToInt32(Session["ID"]);
            List<project> list = new List<project>();

            var projects = db.mangaed_projects.Where(s => s.pm_id == pm_id && s.state_id==7);
            if (projects.Count() != 0)
            {
                foreach (var x in projects)
                {
                    list.Add(db.projects.Find(x.project_id));
                }
            }
            return View(list);
        }

        public ActionResult getProjectmembers(int? id)
        {

            List<user> list = new List<user>();

            var project_members = db.project_members.Where(s => s.project_id == id);
            if (project_members.Count() != 0)
            {
                foreach (var x in project_members)
                {
                    list.Add(db.users.Find(x.member_id));
                }
            }
            return View(list);
        }


        public ActionResult DeleteMember(int ?id)
        {

            project_members result = (from p in db.project_members
                              where p.member_id == id
                              select p).SingleOrDefault();


            db.project_members.Remove(db.project_members.Find(result.Id));
            db.SaveChanges();
            return RedirectToAction("getProjectmembers");
        }

        public ActionResult DeleteProject(int? id)
        {


            mangaed_projects result = (from p in db.mangaed_projects
                                      where p.project_id == id
                                      select p).FirstOrDefault();
            Session["myid"] = result.Id;
            project po = (from p in db.projects
                          where p.Id == result.project_id
                          select p).SingleOrDefault();
            po.assigend_state_id = 3;

            db.mangaed_projects.Remove(db.mangaed_projects.Find(result.Id));

            db.SaveChanges();



            return RedirectToAction("getPmProjects");
        }

	}
}

/*
 
            int result_com = DateTime.Compare(result.deliver_date, DateTime.Now);
            string relationship;

            if (result_com < 0)
                relationship = "is earlier than";
            else if (result_com == 0)
                relationship = "is the same time as";
            else
                relationship = "is later than";*/