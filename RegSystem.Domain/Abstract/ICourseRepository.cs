using System.Collections.Generic;
using RegSystem.Domain.Entities;

namespace RegSystem.Domain.Abstract
{
    public interface ICourseRepository
    {
        IEnumerable<Course> Courses { get; }
        void SaveCourse(Course course);

        Course DeleteCourse(int courseID);
    }
}
