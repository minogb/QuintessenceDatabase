using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QuintessenceDataServer.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item List Page
        public ActionResult Index(int id = 1) {
            return View(Models.Item.GetItemPage(id));
        }

        // GET: Item/Details/5
        public ActionResult Details(int? id) {
            if (!id.HasValue) {
                return RedirectToAction("Index");
            }
            Models.Item item = new Models.Item(id.Value);
            return View(item);
        }

        // GET: Item/Create
        //TODO: authintication
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        //TODO: authintication
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Edit/5
        //TODO: authintication
        public ActionResult Edit(int? id) {
            if (!id.HasValue) {
                return RedirectToAction("Index");
            }
            Models.Item item = new Models.Item(id.Value);
            return View(item);
        }

        // POST: Item/Edit/5
        //TODO: authintication
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Delete/5
        //TODO: authintication
        public ActionResult Delete(int? id) {
            if (!id.HasValue) {
                return RedirectToAction("Index");
            }
            Models.Item.Delete(id.Value);
            return RedirectToAction("Index");
        }

        public JsonResult Slots(int? id = null) {
            try {
                if (id.HasValue) {
                    return Json(Models.Item.GetSlotEnum(id), JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(Models.Item.GetSlotEnum(null), JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e) {
                var errorModel = new { message = e.ToString(), resutl="Error" };
                return Json(errorModel, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Item/Delete/5
        //TODO: authintication
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
