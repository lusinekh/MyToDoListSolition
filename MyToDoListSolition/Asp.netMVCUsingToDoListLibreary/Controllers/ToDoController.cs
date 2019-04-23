using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoListClasseLibreary;

namespace Asp.netMVCUsingToDoListLibreary.Controllers
{
    public class ToDoController : Controller
    {
        private ToDoItem itms = new ToDoItem();
        private ToDoList model = new ToDoList();

        public List<ToDoItem> GetToDoList;
        string connetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ToDoList;Integrated Security=True;Pooling=False";
        // GET: ToDo
        public ActionResult Index()
        {
            using (SqlConnection cnn = model.Connetion(connetionString))
            {
                GetToDoList = model.GetAllItms(cnn);

            }
            return View(GetToDoList);
        }

        public ActionResult Completed()
        {
            using (SqlConnection cnn = model.Connetion(connetionString))
            {
                var result = model.GetCompletedItems(cnn);
                return View(result);
            }     
          
        }

        public ActionResult Active()
        {
            using (SqlConnection cnn = model.Connetion(connetionString))
            {
                var result = model.GetActiveItems(cnn);
                return View(result);
            }
        }

        // GET: ToDo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDo/Create
        [HttpPost]
        public ActionResult Create(ToDoItem itm)
        {
            try
            {
                using (SqlConnection cnn = model.Connetion(connetionString))
                {
                    GetToDoList = model.GetAllItms(cnn);
                    model.AddToDoListItm(cnn, itm.Discript, itm.IsDone);
                    GetToDoList = model.GetAllItms(cnn);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDo/Edit/5
        public ActionResult Edit(int id)
        {
            using (SqlConnection cnn = model.Connetion(connetionString))
            {
                GetToDoList = model.GetAllItms(cnn);
                var item = GetToDoList.Single(m => m.Id == id);
                return View(item);
            }
        }

        // POST: ToDo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ToDoItem itms)
        {
            try
            {

                using (SqlConnection cnn = model.Connetion(connetionString))
                {
                    model.UpdatToDoListItm(cnn, itms.IsDone, id);
                    GetToDoList = model.GetAllItms(cnn);
                    var itm = GetToDoList.Single(m => m.Id == id);

                    if (TryUpdateModel(itm))
                    {

                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: ToDo/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection cnn = model.Connetion(connetionString))
            {
                GetToDoList = model.GetAllItms(cnn);
                var item = GetToDoList.Single(m => m.Id == id);
                return View(item);
            }            
        }
        // POST: ToDo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ToDoItem itms)
        {
            try
            {
                using (SqlConnection cnn = model.Connetion(connetionString))
                {
                    model.DelateDoListItm(cnn, id);

                }
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
