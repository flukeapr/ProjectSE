using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectSE.Models;

namespace ProjectSE.Controllers
{
    public class TechnicianController : Controller
    {
        DatabaseSEEntities db = new DatabaseSEEntities();
        // GET: Technician
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListRepairTech()
        {



            var userName = Session["UserNameT"] as string;
            
            return View(db.Repairs.ToList());
        }
        public ActionResult UpdateStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Repair repair = db.Repairs.Find(id);

            if (repair == null)
            {
                return HttpNotFound();
            }

            return View(repair);
        }
        [HttpPost]
        public ActionResult UpdateStatus(int? id,string status)
        {
            var userName = Session["UserNameT"] as string;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Repair  repair = db.Repairs.Find(id);

            if (ModelState.IsValid)
            {

                if (repair.status.Equals("รอดำเนินการ"))
                {
                    repair.status = "รับเรื่อง";
                    repair.userNameT = userName;
                }
                else if (repair.status.Equals("รับเรื่อง"))
                {
                    repair.status = "เดินทาง";

                }
                else if (repair.status.Equals("เดินทาง"))
                {
                    repair.status = "ถึงแล้ว";

                }
                else if (repair.status.Equals("ถึงแล้ว"))
                {
                    repair.status = "สำเร็จ";

                }
                db.SaveChanges();
                db.Entry(repair).Property(r => r.status).IsModified = true;
                db.Entry(repair).Property(r => r.userNameT).IsModified = true;
                return RedirectToAction("ListRepairTech",db.Repairs.ToList().Where(a => a.userNameT == userName));
            

        }
            if (repair == null)
            {
                return HttpNotFound();
            }
            return View(repair);
        }
       
        public ActionResult AllRepair()
        {
            return View();
        }


    }
}