using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentManagement_ASP.NET_MCV5.Models;

namespace StudentManagement_ASP.NET_MCV5.Controllers
{
    public class LecturersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lecturers
        public async Task<ActionResult> Index()
        {
            return View(await db.Lecturers.ToListAsync());
        }

        //// GET: Lecturers/Details/5
        //public async Task<ActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Lecturer lecturer = await db.Users.FindAsync(id);
        //    if (lecturer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lecturer);
        //}

        //// GET: Lecturers/Create
        //public ActionResult Create()
        //{
        //    ViewBag.FacultyId = new SelectList(db.Faculties, "Id", "Name");
        //    return View();
        //}

        //// POST: Lecturers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,BirthDay,Address,FirstName,LastName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,LecturerCode,HireDate,FacultyId,IsDeleted")] Lecturer lecturer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(lecturer);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.FacultyId = new SelectList(db.Faculties, "Id", "Name", lecturer.FacultyId);
        //    return View(lecturer);
        //}

        //// GET: Lecturers/Edit/5
        //public async Task<ActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Lecturer lecturer = await db.Users.FindAsync(id);
        //    if (lecturer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.FacultyId = new SelectList(db.Faculties, "Id", "Name", lecturer.FacultyId);
        //    return View(lecturer);
        //}

        //// POST: Lecturers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,BirthDay,Address,FirstName,LastName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,LecturerCode,HireDate,FacultyId,IsDeleted")] Lecturer lecturer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(lecturer).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.FacultyId = new SelectList(db.Faculties, "Id", "Name", lecturer.FacultyId);
        //    return View(lecturer);
        //}

        //// GET: Lecturers/Delete/5
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Lecturer lecturer = await db.Users.FindAsync(id);
        //    if (lecturer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lecturer);
        //}

        //// POST: Lecturers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{
        //    Lecturer lecturer = await db.Users.FindAsync(id);
        //    db.Users.Remove(lecturer);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
