using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementOfStudentGradesWpfApp.Models
{
    public class Group
    {
        [Key]
        public int IdGroup{ get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
