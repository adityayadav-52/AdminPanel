using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateCrud.DB_Connect;
using TemplateCrud.Models;

namespace TemplateCrud.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {

            MytableEntities dbobj = new MytableEntities();
            List<EmpRes> obj = new List<EmpRes>();
            var res = dbobj.MyInfoes.ToList();
            foreach (var item in res)
            {
                obj.Add(new EmpRes
                {
                    Id=item.Id,
                    Name=item.Name,
                    Email=item.Email,
                    Mobile=item.Mobile,
                    City=item.City
                });
            }
            return View(obj);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(EmpRes empobj)
        {
            MytableEntities dbobj = new MytableEntities();
            MyInfo tblobj = new MyInfo();
            tblobj.Id = empobj.Id;
            tblobj.Name = empobj.Name;
            tblobj.Email = empobj.Email;
            tblobj.Mobile = empobj.Mobile;
            tblobj.City = empobj.City;

            if (empobj.Id == 0) 
            {
                dbobj.MyInfoes.Add(tblobj);
                dbobj.SaveChanges();
            }
            else
            {
                dbobj.Entry(tblobj).State = System.Data.Entity.EntityState.Modified;
                dbobj.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            MytableEntities dbobj = new MytableEntities();
            var del = dbobj.MyInfoes.Where(a => a.Id == id).First();
            dbobj.MyInfoes.Remove(del);
            dbobj.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            MytableEntities dbobj = new MytableEntities();
            EmpRes empobj = new EmpRes();
            var edit = dbobj.MyInfoes.Where(a => a.Id == id).First();
            empobj.Id = edit.Id;
            empobj.Name = edit.Name;
            empobj.Email = edit.Email;
            empobj.Mobile = edit.Mobile;
            empobj.City = edit.City;

            ViewBag.id = edit.Id;
            return View("Add",empobj);
        }
    }
}