using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegSystem.Domain.Entities
{
   public class Course
    {
        public string CourseID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Section { get; set; }
        public string Credit { get; set; }
        public string Time { get; set; }
        public string Day { get; set; }

    }
}
