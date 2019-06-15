using Newtonsoft.Json;
using StudentManagement_ASP.NET_MCV5.Models;
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
            foreach (Class item in classes)
            {
                item.Faculty = db.Faculties.Where(c => c.Id == item.FacultyId).FirstOrDefault();
                item.Lecturer = db.Lecturers.Where(c => c.Id == item.LecturerId).FirstOrDefault();
            }

            return View(classes);
        }

        // GET: Class/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.Include(c => c.StudentClass).Where(c => c.Id == id).FirstOrDefaultAsync();
            @class.Faculty = db.Faculties.Where(c => c.Id == @class.FacultyId && c.IsDeleted == false).First();
            @class.Lecturer = db.Lecturers.Where(c => c.Id == @class.LecturerId && c.IsDeleted == false).First();


            List<StudentClass> studentClasses = db.StudentClasses.Include(c => c.Student).Where(c => c.ClassId == @class.Id).ToList();
            @class.StudentClass = studentClasses;

            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Class/Create
        [Authorize]
        public ActionResult Create()
        {
            ClassViewModel classViewModel = new ClassViewModel()
            {
                Faculties = db.Faculties.Where(c => c.IsDeleted == false).ToList(),
                Lecturers = db.Lecturers.Where(c => c.IsDeleted == false).ToList()
            };

            List<SelectListItem> LecturerList = new List<SelectListItem>();
            foreach (Lecturer item in classViewModel.Lecturers)
            {
                LecturerList.Add(new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.LecturerCode });
            }
            ViewBag.LecturerList = LecturerList;

            return View(classViewModel);
        }

        // POST: Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
                @class.Lecturer = db.Lecturers.Where(c => c.LecturerCode == classViewModel.LecturerId).First();
                @class.FacultyId = @class.Faculty.Id;
                @class.LecturerId = @class.Lecturer.Id;


                db.Classes.Add(@class);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(classViewModel);
        }

        // GET: Class/Edit/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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

        // GET: Class/GetStudentById/5
        [Authorize]
        public JsonResult GetStudentById(string id)
        {
            Student student = new Student();
            if (id != null)
            {
                student = db.Students.Where(c => c.StudentCode == id).FirstOrDefault();
            }

            if (student != null)
            {
                Dictionary<string, string> studentInfo = new Dictionary<string, string>
                {
                { "StudentCode", student.StudentCode },
                { "Name", student.FirstName + " " + student.LastName },
                { "Email", student.Email},
                { "Birthday", student.BirthDay.Value.ToString("dd/MM/yyyy")},
                { "Address", student.Address}
                };
                return Json(JsonConvert.SerializeObject(studentInfo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        // GET: Class/Delete/5
        [Authorize]
        public async Task<ActionResult> AddStudentToClass(string StudentCode, int ClassId)
        {
            try
            {
                Class @class = await db.Classes.Where(c => c.Id == ClassId).FirstOrDefaultAsync();
                Student student = await db.Students.Where(c => c.StudentCode == StudentCode).FirstOrDefaultAsync();
                if (@class != null && student != null)
                {
                    StudentClass studentClass = await db.StudentClasses.Where(c => c.ClassId == ClassId && c.StudentId == student.Id).FirstOrDefaultAsync();
                    if (studentClass == null)
                    {
                        studentClass = new StudentClass
                        {
                            ClassId = @class.Id,
                            StudentId = student.Id
                        };
                        db.StudentClasses.Add(studentClass);
                    }
                    else
                    {
                        studentClass.IsDeleted = false;
                        db.Entry(studentClass).State = EntityState.Modified;
                    }
                    await db.SaveChangesAsync();
                }
                //@class.Faculty = db.Faculties.Where(c => c.Id == @class.FacultyId).First();
                //@class.Lecturer = db.Lecturers.Where(c => c.Id == @class.LecturerId).First();

                //@class.IsDeleted = false;
                //db.Entry(@class).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                ViewBag.ClassId = @class.Id;
                return View(student);
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
        }

        // GET: Class/Delete/5
        [Authorize]
        public async Task<ActionResult> RemoveStudentFromClass(string StudentId, int ClassId)
        {
            try
            {
                StudentClass studentClass = await db.StudentClasses.Include(c=>c.Class).Include(c=>c.Student).Where(c => c.ClassId == ClassId && c.StudentId == StudentId).FirstOrDefaultAsync();

                if (studentClass != null)
                {
                    return View(studentClass);
                }

                return RedirectToAction("Detail", "Classes", ClassId);
                //@class.Faculty = db.Faculties.Where(c => c.Id == @class.FacultyId).First();
                //@class.Lecturer = db.Lecturers.Where(c => c.Id == @class.LecturerId).First();

                //@class.IsDeleted = false;
                //db.Entry(@class).State = EntityState.Modified;
                //await db.SaveChangesAsync();                
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("RemoveStudentFromClass")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> RemoveStudentFromClassConfirmed(string StudentId, int ClassId)
        {
            try
            {
                StudentClass studentClass = await db.StudentClasses.Include(c => c.Class).Include(c => c.Student).Where(c => c.ClassId == ClassId && c.StudentId == StudentId).FirstOrDefaultAsync();
                
                studentClass.IsDeleted = true;
                db.Entry(studentClass).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Details", "Classes", new { id = ClassId });
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
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
