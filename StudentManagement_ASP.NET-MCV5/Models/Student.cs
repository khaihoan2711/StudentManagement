using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement_ASP.NET_MCV5.Models
{
    public class Student : ApplicationUser
    {
        //Student identity number
        [Key]
        public string StudentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}