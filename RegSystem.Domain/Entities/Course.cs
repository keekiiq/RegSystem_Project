using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RegSystem.Domain.Entities
{
   public class Course
    {
        [HiddenInput(DisplayValue = false)]
        public int CourseID { get; set; }
        [Required(ErrorMessage = "Please enter a course name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter a section")]
        public string Section { get; set; }
        [Required(ErrorMessage = "Please enter a course credit")]
        public string Credit { get; set; }
        [Required(ErrorMessage = "Please enter a course time")]
        public string Time { get; set; }
        [Required(ErrorMessage = "Please enter day")]
        public string Day { get; set; }

    }
}
