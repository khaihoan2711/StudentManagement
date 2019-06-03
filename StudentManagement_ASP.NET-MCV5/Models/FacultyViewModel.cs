using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement_ASP.NET_MCV5.Models
{
    public class FacultyViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Dean { get; set; }
        public string AssociateDean { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
    }
}