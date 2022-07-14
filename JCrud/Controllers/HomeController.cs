using JCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCrud.Controllers
{
    public class HomeController : Controller
    {
        DemoEntities1 db = new DemoEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read(string id)
        {
            var list = db.practices.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult Create(practice std)
        {
            var data = db.practices.Add(std);
            db.SaveChanges();
            string message = "SUCCESS";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var data = db.practices.Find(id);
            db.practices.Remove(data);
            db.SaveChanges();
            string message = "successfully deleted";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }


        public JsonResult Edit(int pid)
        {
            var data = db.practices.FirstOrDefault(model => model.id == pid);
            return Json(data , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(practice model)
        {
            string Message = "Data Successfully Updated";
            if (!ModelState.IsValid)
            {
                var errorlist = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage.ToList());
                return Json(data: new { success = false, Message = "Validation Error", Errorlist = errorlist });

            }

            practice obj = db.practices.FirstOrDefault(x => x.id == model.id) ?? new practice();

            obj.id = model.id;
            obj.name = model.name;
            obj.address = model.address;
            obj.phone_number = model.phone_number;

            if (model.id == 0)
            {
                Message = "Data Successfully Added";
                db.practices.Add(obj);
            }

            db.SaveChanges();
            return Json(data: new { Success = true, Message = Message }, JsonRequestBehavior.AllowGet);
        }
    }
}