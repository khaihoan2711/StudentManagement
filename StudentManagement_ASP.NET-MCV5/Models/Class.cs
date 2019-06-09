using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement_ASP.NET_MCV5.Models
{
    public class Class
    {
        public Class()
        {
            //HoanLK If someone uncomments this line, a class must have at least 1 student when insert sample data
            //this.Students = new HashSet<Student>();
            this.Faculty = new Faculty();
            this.Lecturer = new Lecturer();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string FacultyId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }

        public string LecturerId { get; set; }
        //[System.ComponentModel.DataAnnotations.Schema.ForeignKey("LecturerId")]
        public virtual Lecturer Lecturer { get; set; }

        public string SchoolYear { get; set; }
        public DegreeLevels DegreeLevel { get; set; }
        public TypeOfEducations TypeOfEducation { get; set; }
        
        public bool IsFinished { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }

    public enum DegreeLevels
    {
        AssociateDegree,
        BachelorDegree,
        MasterDegree
    }

    public enum TypeOfEducations
    {
        FullTime,
        InServices
    }
}