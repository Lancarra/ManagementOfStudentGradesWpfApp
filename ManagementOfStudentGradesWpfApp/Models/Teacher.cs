using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementOfStudentGradesWpfApp.Models
{
    public class Teacher
    {
        [Key]
        public int IdTeacher { get; set; }
        public string FullName { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
