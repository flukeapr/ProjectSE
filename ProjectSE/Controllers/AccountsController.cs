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

            return View();
        }
        [HttpPost]
        public ActionResult Login(Account model)
        {
            
            
              var user = db.Accounts.FirstOrDefault(x => x.userName == model.userName && x.password == model.password );
                if (user != null)
                {
                    if (user.role == 1)
                    {
                    return RedirectToAction("Index", "Home");
                }
                    else if (user.role == 3)
                    {
                        return RedirectToAction("About", "Home");
                    }
                else
                {
                    
                    return View();
                }
                }
                
                return View();
            }  
           
        }
    }

