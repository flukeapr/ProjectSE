using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        public ActionResult UpdateStatus(int? id, string status, string description, HttpPostedFileBase file,Repair model)
        {
            var userName = Session["UserNameT"] as string;
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Repair  repair = db.Repairs.Find(id);
            
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/DesT-Pic"), fileName);
                file.SaveAs(path);
                repair.desT_picture = fileName;
            }
            if (ModelState.IsValid)
            {

                

                switch (repair.status)
                {
                    case "รอดำเนินการ":
                        repair.status = "รับเรื่อง";
                        repair.userNameT = userName;
                        var userIdObject = Session["UserId"];
                        var TechId = (int)userIdObject;
                        repair.tech_id = TechId;
                        break;
                    case "รับเรื่อง":
                        repair.status = "เดินทาง";
                        break;
                    case "เดินทาง":
                        repair.status = "ถึงแล้ว";
                        break;
                    case "ถึงแล้ว":
                        repair.status = status;
                        repair.desT = description;
                        break;
                    default:
                        // สถานะอื่น ๆ ไม่ต้องทำอะไร
                        break;
                }
               
                db.SaveChanges();
                db.Entry(repair).Property(r => r.desT).IsModified = true;
                db.Entry(repair).Property(r => r.desT_picture).IsModified = true;
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
       
        public ActionResult AllRepairTech()
        {
            var userName = Session["UserNameT"] as string;
            return View(db.Repairs.ToList().Where(x=>x.userNameT == userName ));
        }
        public ActionResult ReportTech()
        {
            var userName = Session["UserNameT"] as string;
            ViewBag.UserName = userName;
            return View(db.Repairs.ToList().Where(x => x.userNameT == userName));
        }

    }
}