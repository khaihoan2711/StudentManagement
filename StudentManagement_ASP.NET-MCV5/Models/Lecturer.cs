using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement_ASP.NET_MCV5.Models
{
    public class Lecturer : ApplicationUser
    {
        [Key]
        public string LecturerId { get; set; }
        public DateTime HireDate { get; set; }

        public string FacultyId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }
        

    }
}