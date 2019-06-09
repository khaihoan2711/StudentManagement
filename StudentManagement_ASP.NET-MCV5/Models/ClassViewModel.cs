using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement_ASP.NET_MCV5.Models
{
    public class ClassViewModel
    {
        public ClassViewModel()
        {

        }

        public ClassViewModel(Class @class)
        {
            this.Id = @class.Id;
            this.Name = @class.Name;
            this.FacultyId = @class.FacultyId;
            this.LecturerId = @class.LecturerId;
            this.SchoolYear = @class.SchoolYear;
            this.DegreeLevel = @class.DegreeLevel;
            this.TypeOfEducation = @class.TypeOfEducation;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public string FacultyId { get; set; }
        public ICollection<Faculty> Faculties { get; set; }

        public string LecturerId { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }

        public string SchoolYear { get; set; }
        public DegreeLevels DegreeLevel { get; set; }
        public TypeOfEducations TypeOfEducation { get; set; }
    }
}