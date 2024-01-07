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
            var userName = Session["UserName"] as string;
            var TypeRepairCategories = new List<String>
        {
            "น้ำปะปา" ,
            "ไฟฟ้า" ,
            "เฟอนิเจอร์" ,
            "เครื่องใช้ไฟฟ้า" ,
            "การทาสีและซ่อมแซมพื้น" ,
            "ซ่อมแซมทั่วไป" ,
            "โครงสร้างและประตู-หน้าต่าง"


        };
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
            ViewBag.selectedTypeRepair = TypeRepairCategories;

            var repair = new Repair
            {
                selectedTypeRepair = TypeRepairCategories,
                selectedbuilding_Name = buildingNameCategories,
                selectedFloor = floorCategories,
                userNameR = userName
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
                var userName = Session["UserName"] as string;
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
                    status = "รอดำเนินการ",
                    userNameR = userName
                };
            

                db.Repairs.Add(repair);
                db.SaveChanges();
                return View();
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
            var TypeRepairCategories = new List<String>
        {
            "น้ำปะปา" ,
            "ไฟฟ้า" ,
            "เฟอนิเจอร์" ,
            "เครื่องใช้ไฟฟ้า" ,
            "การทาสีและซ่อมแซมพื้น" ,
            "ซ่อมแซมทั่วไป" ,
            "โครงสร้างและประตู-หน้าต่าง"


        };




            model.selectedbuilding_Name = buildingNameCategories;
            model.selectedFloor = floorCategories;
            model.selectedTypeRepair = TypeRepairCategories;
            ViewBag.selectedTypeRepair = TypeRepairCategories;
            ViewBag.selectedFloorbuilding_Name = buildingNameCategories;
            ViewBag.selectedFloor = floorCategories;
            return View(model);


        }
       public ActionResult ListRepair()
        {
            var userName = Session["UserName"] as string;
            return View(db.Repairs.ToList().Where(list => list.userNameR == userName));
        }
    }
}