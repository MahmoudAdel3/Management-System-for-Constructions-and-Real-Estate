using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IA.Models;

namespace Test.Controllers
{
    public class TeamLeaderController : Controller
    {
        Database2Entities3 db = new Database2Entities3();
        
        
        // here show all watiing request
        public ActionResult Index()
        {

            return View(db.requests.Where(x => x.state_id == 5));
        }
        //accept request
        public ActionResult Accept(int? id)
        {
            request request = db.requests.Find(id);
        
            request.state_id = 5;
            db.SaveChanges();
           
            int projectId = request.project_id;
            project_members member=new project_members();
            member.project_id = projectId;
            member.member_id = request.reciever_id;
            db.project_members.Add(member);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        //refuse request
        public ActionResult Refuse(int? id)
        {
            request request = db.requests.Find(id);
            request.state_id = 8;
            db.SaveChanges();
            return RedirectToAction("Index");
        }





        //All the Current projects that he currently joining it
        public ActionResult currentProjects()
        {
            
            int id = Convert.ToInt32(Session["ID"]);
            List<project> list = new List<project>();
            var projects=db.project_members.Where(s => s.member_id == id);
            if(projects.Count()!=0)
            {
                foreach (var x in projects)
                {
                    list.Add(db.projects.Find(x.project_id));
                }
            }
            db.SaveChanges();
            return View(list);
        }




        //working with jonir developers here 
        public ActionResult AddJuniors(int? id)
        {
            return View(db.users.Where(x => x.type_id == 3));
        }
        
        [HttpPost]
        public ActionResult SendRequest(request request)
        {
            request.project_id = Convert.ToInt32(Session["project_id"]);
            request.reciever_id = Convert.ToInt32(Session["reciver_id"]);
            request.sender_id = Convert.ToInt32(Session["ID"]);
            request.state_id = 1; // 5 mean waiting 
            db.requests.Add(request);
            db.SaveChanges();
            return RedirectToAction("AddJuniors");
        }
        public ActionResult SendRequest(int? id)
        {
            Session["reciver_id"] = id;
            return View();
        }



        public ActionResult DeleteJunior( int? juniorId)
        {

            request request = new request();
            request.project_id = Convert.ToInt32(Session["project_id"]);
            //herererrer
            request.content = new StringBuilder().Append(juniorId).ToString();
            request.reciever_id = Convert.ToInt32(Session["pm_id"]);
            request.state_id = 1;
            request.sender_id = Convert.ToInt32(Session["user_id"]);
            db.requests.Add(request);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = Convert.ToInt32(Session["project_id"]) });

        }
        

        public ActionResult GiveFeedback(int? id)
        {
            Session["junior_id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult GiveFeedback(feedback feedback)
        {
            feedback.member_id = Convert.ToInt32(Session["junior_id"]);
            feedback.project_id = Convert.ToInt32(Session["project_id"]);
            feedback.tl_id= Convert.ToInt32(Session["user_id"]);
           
            var members =db.project_members.Where(s => s.project_id == feedback.project_id);
            if (members.Count() != 0)
            {

                foreach (var x in members)
                {
                    user u = db.users.Find(x.member_id);

                    if(u.type_id==2)
                    {
                        feedback.pm_id = x.member_id;
                        break;
                    }
                }
            }
            
            db.feedbacks.Add(feedback);
            db.SaveChanges();

            return RedirectToAction("Details", new { id =Convert.ToInt32(Session["project_id"]) });
        }

        public ActionResult Details(int? id)
        {
            Session["project_id"] = id;
            project project=new project();

            var members = db.project_members.Where(s => s.project_id == id);
            if (members.Count() != 0)
            {

                foreach (var x in members)
                {
                    user u = db.users.Find(x.member_id);

                    if (u.type_id == 2)
                    {
                        Session["pm_id"] = x.member_id;
                        break;
                    }
                }
            }

            
            return View(members);
        }
        public ActionResult Statistical()
        {

            return View();
        }

    }
}