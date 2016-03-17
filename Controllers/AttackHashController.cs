using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class AttackHashController : Controller
    {
        // GET: AttackHash
        public ActionResult birthdayAttack()
        {
            return View();
        }

        // GET: AttackHash/Details/5
        public ActionResult colGen(Models.colGen cg)
        {
            return View(cg);
        }

        // GET: AttackHash/Details/5
        public ActionResult bdaeAtk(Models.bdaeAtk ba)
        {
            return View(ba);
        }

        // GET: AttackHash/Create
        public ActionResult colResistBench(Models.colResistBench cb)
        {
            return View(cb);
        }



        // POST: AttackHash/Create
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

        // GET: AttackHash/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AttackHash/Edit/5
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

        // GET: AttackHash/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AttackHash/Delete/5
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
