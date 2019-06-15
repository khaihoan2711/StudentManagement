using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement_ASP.NET_MCV5.Models
{
    public class StudentClass
    {
        [Key, Column(Order = 1)]
        public int ClassId { get; set; }
        public virtual Class @Class { get; set; }
        [Key, Column(Order = 2)]
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public bool IsDeleted { get; set; }

    }
}