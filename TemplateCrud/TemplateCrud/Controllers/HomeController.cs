using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TemplateCrud.DB_Connect;
using TemplateCrud.Models;

namespace TemplateCrud.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(UserInfoModel userobj)
        {
            MytableEntities dbobj = new MytableEntities();

            var UserRes = dbobj.UserInfoes.Where(a => a.Email == userobj.Email).FirstOrDefault();

            if (UserRes == null)
            {
                TempData["Invalid"] = "Email not found or Invallid User";
            }

            else
            {
                if (UserRes.Email == userobj.Email && UserRes.Password == userobj.Password) 
                {

                    FormsAuthentication.SetAuthCookie(UserRes.Email, false);


                    Session["UserName"] = UserRes.Name;
                    Session["UserEmail"] = UserRes.Email;


                    return RedirectToAction("IndexDashBoard","Home");
                }

                else
                {
                    TempData["Wrong"] = "Wrong Email or Password";
                    return View();
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult IndexDashBoard()
        {
            return View();
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public ActionResult EmpTable()
        {
            MytableEntities dbobj = new MytableEntities();
            List<EmpRes> obj = new List<EmpRes>();
            var res = dbobj.MyInfoes.ToList();
            foreach (var item in res)
            {
                obj.Add(new EmpRes
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Mobile = item.Mobile,
                    City = item.City
                });
            }
            return View(obj);
        }

        [HttpGet]
        public ActionResult UserReg()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserReg(UserInfoModel userobj)
        {
            MytableEntities dbobj = new MytableEntities();
            UserInfo tblobj = new UserInfo();
            tblobj.Id = userobj.Id;
            tblobj.Name = userobj.Name;
            tblobj.Email = userobj.Email;
            tblobj.Password = userobj.Password;

            dbobj.UserInfoes.Add(tblobj);
            dbobj.SaveChanges();


            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();

        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}