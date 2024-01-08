using ProjectSE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSE.Controllers
{
    
    public class AdminController : Controller
    {
        DatabaseSEEntities db = new DatabaseSEEntities();

        technicianAjax _dbContext; 
         public AdminController()
         { 
         _dbContext = new Models.technicianAjax(); 
         }
        public ActionResult GetTechnician()
         { 
         var tblTechnician = _dbContext.Technician.ToList(); 
         return Json(tblTechnician, JsonRequestBehavior.AllowGet); 
         }

        public ActionResult Get(int id)
        {
           
                var technician = _dbContext.Technician.ToList().Find(x => x.technician_Id == id);
                return Json(technician, JsonRequestBehavior.AllowGet);
            
         
            
        }
        [HttpPost] 
         public ActionResult Create([Bind(Exclude = "ID")] Technician technician)
         { 
         if (ModelState.IsValid) 
         { 
             _dbContext.Technician.Add(technician); 
             _dbContext.SaveChanges(); 
         } 
         return Json(technician, JsonRequestBehavior.AllowGet); 
         }

         [HttpPost]
         public ActionResult Update(Technician technician)
         {
             if (ModelState.IsValid)
                 {
                _dbContext.Entry(technician).State = EntityState.Modified;
                _dbContext.SaveChanges();
                 }
             return Json(technician, JsonRequestBehavior.AllowGet);
             }

         [HttpPost]
         public ActionResult Delete(int id)
         {
            var technician = _dbContext.Technician.ToList().Find(x => x.technician_Id == id);

             if (technician != null)
                 {
                _dbContext.Technician.Remove(technician);
                _dbContext.SaveChanges();
                 }
             return Json(technician, JsonRequestBehavior.AllowGet);
             }

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
        public ActionResult ListRepair()
        {
            return View(db.Repairs.ToList());
        }

        public ActionResult RenterMem()
        {
            return View(db.Renters.ToList()) ;
        }























    }
}
         

    