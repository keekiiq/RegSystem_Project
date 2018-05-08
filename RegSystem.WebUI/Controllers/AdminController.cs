using System.Linq;
using System.Web.Mvc;
using RegSystem.Domain.Abstract;
using RegSystem.Domain.Entities;

namespace RegSystem.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ICourseRepository repository;
        public AdminController(ICourseRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View(repository.Courses);
        }

        public ViewResult Edit(int courseId)
        {
            Course course = repository.Courses
                .FirstOrDefault(p => p.CourseID == courseId);
            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCourse(course);
                TempData["message"] = string.Format("{0} has been saved", course.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(course);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Course());
        }

        [HttpPost]
        public ActionResult Delete(int courseId)
        {
            Course deletedCourse = repository.DeleteCourse(courseId);
            if (deletedCourse != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                    deletedCourse.Name);
            }
            return RedirectToAction("Index");
        }

    }
}