using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectSE.Models;


namespace ProjectSE.Controllers
{
    public class AccountsController : Controller
    {
        private DatabaseSEEntities db = new DatabaseSEEntities();

        // GET: Accounts
        public ActionResult Login()
        {

            return View("Login");
        }
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account model)
        {
            
            
              var user = db.Accounts.FirstOrDefault(x => x.userName == model.userName && x.password == model.password );
            var isPasswordValid = db.Accounts.Any(x => x.password == model.password);

            if (!isPasswordValid)
            {
                ModelState.AddModelError("password", "รหัสผ่านไม่ถูกต้อง");
                return View(model);
            }
            if (user != null)
                {
                Session["UserNameT"] = user.userName;
                Session["UserName"] = user.userName;
                var techId = db.Technicians.Where(t => t.acc_id == user.Id).Select(t => t.technician_Id).FirstOrDefault();
                Session["UserId"] = techId;
                
                if (user.role == 1)
                  {
                    
                    return RedirectToAction("Index", "Admin");
                }
                else if (user.role == 2)
                {
                   
                    return RedirectToAction("Index", "Home");
                }
                else if (user.role == 3)
                    {
                    
                    return RedirectToAction("Index", "Technician");
                    }
                else
                {
                    
                    return View();
                }
                }
                
                return View();
            }
        
        
        public ActionResult LogOff()
        {
            if (Session != null)
            {
                
                Session["UserNameT"] = null;
                Session["UserName"] = null;
                Session["UserId"] = null;
            }
            return RedirectToAction("Login");
        }

        public ActionResult register()
        {

            return View();
        }
    }
    }

