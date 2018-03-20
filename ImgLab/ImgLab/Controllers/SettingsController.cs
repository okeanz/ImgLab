using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImgLab;

namespace ImgLab.Controllers
{
    public class SettingsController : Controller
    {
        private ILDBContext db = new ILDBContext();

        // GET: Settings
        public async Task<ActionResult> Index()
        {
            return View(await db.Settings.ToListAsync());
        }

        // GET: Settings/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingsDataModel settingsDataModel = await db.Settings.FindAsync(id);
            if (settingsDataModel == null)
            {
                return HttpNotFound();
            }
            return View(settingsDataModel);
        }

        // GET: Settings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Settings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Path,LastUpdate,Count")] SettingsDataModel settingsDataModel)
        {
            
            if (ModelState.IsValid)
            {
                DBControl.LoadNew(settingsDataModel, true);
                return PartialView("_ModalSuccess");
            }
            return View(settingsDataModel);
        }

        // GET: Settings/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingsDataModel settingsDataModel = await db.Settings.FindAsync(id);
            if (settingsDataModel == null)
            {
                return HttpNotFound();
            }
            DBControl.Update(settingsDataModel);
            return PartialView("_ModalSuccess");
            //return View(settingsDataModel);
        }

        // POST: Settings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Path,LastUpdate,Count")] SettingsDataModel settingsDataModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(settingsDataModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return PartialView("_ModalSuccess");
            }
            return View(settingsDataModel);
        }

        // GET: Settings/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingsDataModel settingsDataModel = await db.Settings.FindAsync(id);
            if (settingsDataModel == null)
            {
                return HttpNotFound();
            }
            return View(settingsDataModel);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            SettingsDataModel settingsDataModel = await db.Settings.FindAsync(id);
            DBControl.RemoveSetting(settingsDataModel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
