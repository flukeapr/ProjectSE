using Microsoft.Ajax.Utilities;
using ProjectSE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectSE.Controllers
{
    
    public class AdminController : Controller
    {
        DatabaseSEEntities db = new DatabaseSEEntities();

        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TechMember()
        {
           
            return View(db.Technicians.ToList());
        }

        public ActionResult AddTech()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddTech(Technician technichian)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/technician"), fileName);
                    file.SaveAs(path);
                    technichian.image = fileName;
                }


                db.Technicians.Add(technichian);
                db.SaveChanges();
                return View("TechMember", db.Technicians.ToList());
            }
            return View();
        }

        public ActionResult TechEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technician technician = db.Technicians.Find(id);
            if (technician == null)
            {
                return HttpNotFound();
            }
            return View(technician);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TechEdit( Technician technician)
        {
            if (ModelState.IsValid)
            {
                db.Entry(technician).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TechMember", db.Technicians.ToList());
            }
            return View(technician);
        }
        public ActionResult TechDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technician technician = db.Technicians.Find(id);
            if (technician == null)
            {
                return HttpNotFound();
            }
            return View(technician);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("TechDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult TechDeleteConfirmed(int id)
        {
            Technician technician = db.Technicians.Find(id);
            db.Technicians.Remove(technician);
            db.SaveChanges();
            return RedirectToAction("TechMember", db.Technicians.ToList());
        }
        public ActionResult ListRepairAdmin(string sortList, string searchString)
        {
            ViewBag.listSortParm = String.IsNullOrEmpty(sortList) ? "Date_desc" : "";
            var repair = from p in db.Repairs
                         select p;

            if (!String.IsNullOrEmpty(searchString)){
                repair = repair.Where(x => x.nameInform.Contains(searchString) || x.status.Contains(searchString));
            }


            switch (sortList)
            {
                case "Date_desc":
                    repair = repair.OrderByDescending(p => p.date);
                    break;
                default:
                    repair = repair.OrderBy(p => p.repair_Id);
                    break;
            }
            return View(repair);
        }

        public ActionResult RenterMem()
        {
            return View(db.Renters.ToList()) ;
        }

        public ActionResult RenterEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renter renter = db.Renters.Find(id);
            if (renter == null)
            {
                return HttpNotFound();
            }
            return View(renter);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RenterEdit(Renter renter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(renter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("RenterMem", db.Renters.ToList());
            }
            return View(renter);
        }
        public ActionResult RenterDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renter renter = db.Renters.Find(id);
            if (renter == null)
            {
                return HttpNotFound();
            }
            return View(renter);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("RenterDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult RenterDeleteConfirmed(int id)
        {
            Renter renter = db.Renters.Find(id);
            db.Renters.Remove(renter);
            db.SaveChanges();
            return RedirectToAction("RenterMem", db.Renters.ToList());
        }


        public ActionResult ReportAdmin()
        {
            return View();
        }

        public JsonResult GetReportJson()
        {
            var data = db.Repairs.ToList();
            return Json(new { JSONList = data }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEstimateJson()
        {
            var data = db.Estimates.ToList();
            return Json(new { JSONList = data }, JsonRequestBehavior.AllowGet);
        }













    }
}
         

    