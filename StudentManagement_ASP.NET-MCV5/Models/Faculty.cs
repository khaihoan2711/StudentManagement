using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement_ASP.NET_MCV5.Models
{
    public class Faculty
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        //Dean is an Id from Lecturer
        public string Dean { get; set; }
        //AssociateDean is an Id from Lecturer
        public string AssociateDean { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }
    }

    public class FacultyViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Dean { get; set; }
        public string AssociateDean { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
    }
}