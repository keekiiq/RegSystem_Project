using System.Collections.Generic;
using RegSystem.Domain.Entities;
namespace RegSystem.WebUI.Models
{
    public class CoursesListViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}