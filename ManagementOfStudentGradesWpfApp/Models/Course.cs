using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementOfStudentGradesWpfApp.Models
{
    public class Course
    {
        [Key]
        public int IdCourse { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
