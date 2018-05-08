using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Ninject;
using RegSystem.Domain.Entities;
using RegSystem.Domain.Abstract;

namespace RegSystem.WebUI.Controllers
{
    public class CourseController : Controller
    {
        private ICourseRepository repository;

        public CourseController(ICourseRepository courseRepository)
        {
            this.repository = courseRepository;
        }
        public ViewResult List()
        {
            return View(repository.Courses);
        }
    }
}