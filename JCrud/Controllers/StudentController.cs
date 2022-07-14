using JCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCrud.Controllers
{
    public class StudentController : Controller
    {
        DemoEntities db = new DemoEntities();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read(ViewClass id)
        {
            var list = db.Student_Details.Select(a => new ViewClass {
                id = a.id,
                First_Name = a.First_Name, 
                Last_Name = a.Last_Name,
                Email = a.Email,
                City = a.Student_Address_Details.City,
                Street_Address = a.Student_Address_Details.Street_Address,
                Phone_number = a.Phone_number
            }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(ViewClass model)
        {
            Student_Details student = new Student_Details();
            student.First_Name = model.First_Name;
            student.Last_Name = model.Last_Name;
            student.Email = model.Email;
            student.Phone_number = model.Phone_number;

            Student_Address_Details std_det = new Student_Address_Details();
            std_det.City = model.City;
            std_det.Street_Address = model.Street_Address;

            //{
            //    City = model.City,
            //    Street_Address = model.Street_Address,
            //};

            student.Student_Address_Details = std_det;
            db.Student_Details.Add(student);
            db.SaveChanges();
            String message = "SUCCESS";
            return Json(new {Message = message, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var list = db.Student_Details.Find(id);
            db.Student_Details.Remove(list);
            db.SaveChanges();
            string message = "successfully deleted";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }
        public ActionResult Edit(int ID)
        {
            var data = db.Student_Details.FirstOrDefault(model => model.id == ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(ViewClass model)
        {
            string Message = "Data Successfully Updated";
            if (!ModelState.IsValid)
            {
                var errorlist = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage.ToList());
                return Json(data: new { success = false, Message = "Validation Error", Errorlist = errorlist });

            }

            Student_Details obj = db.Student_Details.FirstOrDefault(x => x.id == model.id) ?? new Student_Details();
            obj.id = model.id;
            obj.First_Name = model.First_Name;
            obj.Last_Name = model.Last_Name;
            obj.Email = model.Email;
            obj.Student_Address_Details.City = model.City;
            obj.Student_Address_Details.Street_Address = model.Street_Address;
            obj.Phone_number = model.Phone_number;

            if (model.id == 0)
            {
                Message = "Data Successfully Added";
                db.Student_Details.Add(obj);
            }

            db.SaveChanges();
            return Json(data: new { Success = true, Message = Message }, JsonRequestBehavior.AllowGet);
        }
    }
}