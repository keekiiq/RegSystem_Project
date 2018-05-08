using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Ninject;
using RegSystem.Domain.Entities;
using RegSystem.Domain.Abstract;
using RegSystem.WebUI.Models;

namespace RegSystem.WebUI.Controllers
{
    public class CourseController : Controller
    {
        private ICourseRepository repository;
        public int PageSize = 4;

        public CourseController(ICourseRepository courseRepository)
        {
            this.repository = courseRepository;
        }
        public ViewResult List(int page = 1)
        {
            CoursesListViewModel model = new CoursesListViewModel
            {
                Courses = repository.Courses
                .OrderBy(p => p.CourseID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Courses.Count()
                }
            };
            return View(model);
        }
    }
}