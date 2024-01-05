using ProjectSE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSE.Controllers
{
    public class HomeController : Controller
    {
        DatabaseSEEntities db = new DatabaseSEEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuRepair()
        {
            
            var buildingNameCategories = new List<String>
        {
            "หอหญิง" ,
            "หอชาย" 
           
            
        };
            var floorCategories = new List<String>
        {
            "1" ,
            "2"


        };
            ViewBag.selectedbuilding_Name = buildingNameCategories;
            ViewBag.selectedFloor = floorCategories;


            var repair = new Repair
            {
                selectedbuilding_Name = buildingNameCategories,
                selectedFloor = floorCategories
        };

            return View(repair);
        }
        [HttpPost]

        public ActionResult MenuRepair(Repair model)
        {




            if (ModelState.IsValid)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Repair-image"), fileName);
                    file.SaveAs(path);
                    model.picture = fileName;
                }
                var repair = new Repair
                {
                    nameInform = model.nameInform,
                    topicRepair = model.topicRepair,
                    typeRepair = model.typeRepair,
                    details = model.details,
                    picture = model.picture,
                    phone = model.phone,
                    date = model.date,
                    time = model.time,
                    roomId = model.roomId,
                    building_name = model.building_name,
                    floor = model.floor,

                    status = "รอดำเนินการ"

                };
            

                db.Repairs.Add(repair);
                db.SaveChanges();
                return View("ListRepair");
            }
            var buildingNameCategories = new List<String>
        {
            "หอหญิง" ,
            "หอชาย"


        };

            var floorCategories = new List<String>
        {
            "1" ,
            "2"


        };





            model.selectedbuilding_Name = buildingNameCategories;
            model.selectedFloor = floorCategories;
            ViewBag.selectedFloorbuilding_Name = buildingNameCategories;
            ViewBag.selectedFloor = floorCategories;
            return View(model);


        }
       public ActionResult ListRepair()
        {

            return View(db.Repairs.ToList());
        }
    }
}