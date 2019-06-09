using StudentManagement_ASP.NET_MCV5.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentManagement_ASP.NET_MCV5.Controllers
{
    public class ClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Class
        [Authorize]
        public async Task<ActionResult> Index()
        {
            ICollection<Class> classes = await db.Classes.Include(c => c.Faculty).Include(c => c.Lecturer).Where(c => c.IsFinished == false).ToListAsync();
            foreach (var item in classes)
            {
                item.Faculty = db.Faculties.Where(c => c.Id == item.FacultyId).FirstOrDefault();
                item.Lecturer = db.Lecturers.Where(c => c.Id == item.LecturerId).FirstOrDefault();
            }
            
            return View(classes);
        }

        // GET: Class/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.Include(c => c.Students).Where(c => c.Id == id).FirstOrDefaultAsync();
            @class.Faculty = db.Faculties.Where(c => c.Id == @class.FacultyId && c.IsDeleted == false).First();
            @class.Lecturer = db.Lecturers.Where(c => c.Id == @class.LecturerId && c.IsDeleted == false).First();
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            ClassViewModel classViewModel = new ClassViewModel();
            classViewModel.Faculties = db.Faculties.Where(c => c.IsDeleted == false).ToList();
            classViewModel.Lecturers = db.Lecturers.Where(c => c.IsDeleted == false).ToList();

            List<SelectListItem> LecturerList = new List<SelectListItem>();
            foreach (Lecturer item in classViewModel.Lecturers)
            {
                LecturerList.Add(new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.LecturerId });
            }
            ViewBag.LecturerList = LecturerList;

            return View(classViewModel);
        }

        // POST: Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,FacultyId,LecturerId,SchoolYear,DegreeLevel,TypeOfEducation")] ClassViewModel classViewModel)
        {
            if (ModelState.IsValid)
            {
                Class @class = new Class
                {
                    Name = classViewModel.Name,
                    FacultyId = classViewModel.FacultyId,
                    //LecturerId = classViewModel.LecturerId,
                    DegreeLevel = classViewModel.DegreeLevel,
                    SchoolYear = classViewModel.SchoolYear,
                    TypeOfEducation = classViewModel.TypeOfEducation
                };

                @class.Faculty = db.Faculties.Where(c => c.Id == classViewModel.FacultyId).First();
                @class.Lecturer = db.Lecturers.Where(c => c.LecturerId == classViewModel.LecturerId).First();
                @class.FacultyId = @class.Faculty.Id;
                @class.LecturerId = @class.Lecturer.Id;


                db.Classes.Add(@class);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(classViewModel);
        }

        // GET: Class/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.FindAsync(id);

            ClassViewModel classViewModel = new ClassViewModel(@class)
            {
                Lecturers = db.Lecturers.ToList(),
                Faculties = db.Faculties.ToList()
            };
            if (string.IsNullOrEmpty(classViewModel.Name)
                || classViewModel.Lecturers == null
                || classViewModel.Faculties == null)
            {
                return HttpNotFound();
            }
            return View(classViewModel);
        }

        // POST: Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,FacultyId,LecturerId,SchoolYear,DegreeLevel,TypeOfEducation")] ClassViewModel classViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classViewModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(classViewModel);
        }

        // GET: Class/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.Where(c => c.Id == id).FirstOrDefaultAsync();

            @class.Faculty = db.Faculties.Where(c => c.Id == @class.FacultyId).First();
            @class.Lecturer = db.Lecturers.Where(c => c.Id == @class.LecturerId).First();

            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Class @class = await db.Classes.FindAsync(id);
            @class.Faculty = db.Faculties.Where(c => c.Id == @class.FacultyId).First();
            @class.Lecturer = db.Lecturers.Where(c => c.Id == @class.LecturerId).First();
            @class.IsDeleted = true;
            db.Entry(@class).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Class/Restore/5
        public async Task<ActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.Where(c => c.Id == id).FirstOrDefaultAsync();

            @class.Faculty = db.Faculties.Where(c => c.Id == @class.FacultyId).First();
            @class.Lecturer = db.Lecturers.Where(c => c.Id == @class.LecturerId).First();

            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RestoreConfirmed(int id)
        {
            Class @class = await db.Classes.FindAsync(id);
            @class.Faculty = db.Faculties.Where(c => c.Id == @class.FacultyId).First();
            @class.Lecturer = db.Lecturers.Where(c => c.Id == @class.LecturerId).First();

            @class.IsDeleted = false;
            db.Entry(@class).State = EntityState.Modified;
            await db.SaveChangesAsync();
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
