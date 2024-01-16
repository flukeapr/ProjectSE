using ProjectSE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

                var estimate = new Estimate
                {
                    status = "รอประเมิน",

                };

                db.Estimates.Add(estimate);
                db.SaveChanges();

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
                    userNameR = userName,
                    estimate_Id = estimate.estimate_Id
                };


                db.Repairs.Add(repair);
                db.SaveChanges();
                return View("ListRepair", db.Repairs.ToList().Where(list => list.userNameR == userName));
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

        public ActionResult Estimate()
        {
            var result = from r in db.Repairs
                         join e in db.Estimates on r.estimate_Id equals e.estimate_Id
                         select new ProjectSE.Models.EstimateViewModel
                         {
                             estimateId = e.estimate_Id,
                             NameInform = r.nameInform,
                             TypeRepair = r.typeRepair,
                             Details = r.details,
                             Picture = r.picture,
                             Status = r.status,
                             Date = r.date.HasValue ? r.date.Value : default(DateTime),
                             Time = r.time.HasValue ? r.time.ToString() : string.Empty,
                             EstimateStatus = e.status
                         };

            return View(result.ToList());
        }


        public ActionResult Estimation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Estimate estimate = db.Estimates.Find(id);

            if (estimate == null)
            {
                return HttpNotFound();
            }
            var result = from r in db.Repairs
                         join e in db.Estimates on r.estimate_Id equals e.estimate_Id join t in db.Technicians on r.tech_id equals t.technician_Id
                         select new ProjectSE.Models.EstimationViewModel
                         {
                             Technician = t.technicianName,
                             Phone = t.phone,
                             Rate = e.rate
                         };
            


            return View(result);
        }
    }
}