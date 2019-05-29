using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement_ASP.NET_MCV5.Models
{
    public class Subject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}