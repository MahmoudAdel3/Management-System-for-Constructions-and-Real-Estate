using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IA.Models;

namespace IA.Controllers
{
    public class JuniorEngineerController : Controller
    {
        //
        // GET: /JuniorEngineer/
        Database2Entities3 db = new Database2Entities3();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult teamrequest()
        {
            return View(db.requests.Where(u => u.state_id == 1 && u.reciever_id == 4).ToList());
        }
        public ActionResult Acceptrequest(int id)
        {

            request result = (from p in db.requests
                              where p.Id == id
                              select p).SingleOrDefault();

            result.state_id = 5;

            project_members pm = new project_members();
            pm.member_id = Convert.ToInt32(Session["ID"]);
            pm.project_id = result.project_id;
            db.project_members.Add(pm);
            db.SaveChanges();
            return RedirectToAction("teamrequest");
        }
        public ActionResult refuserequest(int id)
        {
            request result = (from p in db.requests
                              where p.Id == id
                              select p).SingleOrDefault();

            result.state_id = 6;
            db.SaveChanges();
            return RedirectToAction("teamrequest");

        }
        public ActionResult currentprojects()
        {
            var id = Convert.ToInt32(Session["ID"]);

            var q = (from t in db.project_members
                     join sc in db.mangaed_projects on t.project_id equals sc.project_id
                     join st in db.projects on t.project_id equals st.Id
                     where t.member_id == id
                     select new { st.Id, st.title, st.creation_date, st.description });
            List<project> list = new List<project>();
            foreach (var t in q)
            {
                list.Add(new project()
                {
                    Id = t.Id,
                    title = t.title,
                    description = t.description,
                    creation_date = t.creation_date,

                });
            }

            return View(list);
        }
        public ActionResult leaveproject(int id)
        {
            int je_id = Convert.ToInt32(Session["ID"]);
            project_members result = (from p in db.project_members
                                      where p.project_id == id && p.member_id == je_id
                                      select p).SingleOrDefault();
            db.project_members.Remove(result);
            db.SaveChanges();
            return RedirectToAction("currentprojects");
        }
        public ActionResult Statistical()
        {

            return View();
        }


    }
}