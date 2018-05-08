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

        public void SaveProduct(Course course)
        {
            if (course.CourseID == 0)
            {
                context.Courses.Add(course);
            }
            else
            {
                Course dbEntry = context.Courses.Find(course.CourseID);
                if (dbEntry != null)
                {
                    dbEntry.Name = course.Name;
                    dbEntry.Description = course.Description;
                    dbEntry.Section = course.Section;
                    dbEntry.Credit = course.Credit;
                    dbEntry.Time = course.Time;
                    dbEntry.Day = course.Day;
                }
            }
            context.SaveChanges();
        }
    }
}
