using System.Collections.Generic;
using RegSystem.Domain.Entities;
using RegSystem.Domain.Abstract;

namespace RegSystem.Domain.Concrete
{
    public class EFCourseRepository : ICourseRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Course> Courses
        {
            get { return context.Courses; }
        }
    }
}
