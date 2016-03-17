using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class BirthdayAttackController : Controller
    {
        // GET: BirthdayAttack
        public ActionResult Index()
        {
            return View();
        }

        // GET: BirthdayAttack/Details/5
        public ActionResult Details(Models.BirthdayAttack BA) 
        {
            return View();
        }

        // GET: BirthdayAttack/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BirthdayAttack/Create
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

        // GET: BirthdayAttack/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BirthdayAttack/Edit/5
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

        // GET: BirthdayAttack/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BirthdayAttack/Delete/5
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
