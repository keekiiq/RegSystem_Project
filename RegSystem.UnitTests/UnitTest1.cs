using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegSystem.Domain.Abstract;
using RegSystem.Domain.Entities;
using RegSystem.WebUI.Controllers;
using RegSystem.WebUI.Models;
using RegSystem.WebUI.HtmlHelpers;
using RegSystem.WebUI.Infrastructure.Abstract;

namespace RegSystem.UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Index_Contains_All_Courses()
        {
            // Arrange - create the mock repository
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new Course[] {
           new Course {CourseID=1,Name="Test1",Description="Test1",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=2,Name="Test2",Description="Test2",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=3,Name="Test3",Description="Test3",Section="3",Credit="1",Time="12.00",Day="Tue"}
});
            // Arrange - create a controller
            AdminController target = new AdminController(mock.Object);
            // Action
            Course[] result = ((IEnumerable<Course>)target.Index().
                ViewData.Model).ToArray();
            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("Test1", result[0].Name);
            Assert.AreEqual("Test2", result[1].Name);
            Assert.AreEqual("Test3", result[2].Name);
        }


[TestMethod]
            public void Can_Login_With_Valid_Credentials()
            {

                Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
                mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

                LoginViewModel model = new LoginViewModel
                {
                    UserName = "admin",
                    Password = "secret"
                };

                AccountController target = new AccountController(mock.Object);

                ActionResult result = target.Login(model, "/MyURL");

                Assert.IsInstanceOfType(result, typeof(RedirectResult));
                Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
            }
            [TestMethod]
            public void Cannot_Login_With_Invalid_Credentials()
            {

                Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
                mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);

                LoginViewModel model = new LoginViewModel
                {
                    UserName = "badUser",
                    Password = "badPass"
                };

                AccountController target = new AccountController(mock.Object);

                ActionResult result = target.Login(model, "/MyURL");

                Assert.IsInstanceOfType(result, typeof(ViewResult));
                Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
            }

    [TestMethod]
        public void Can_Delete_Valid_Courses()
        {
            Course cros = new Course { CourseID = 2, Name = "Test1", Description = "Test1", Section = "3", Credit = "1", Time = "12.00", Day = "Tue" };

            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new Course[] {
           new Course { CourseID = 1, Name = "Test1", Description = "Test1", Section = "3", Credit = "1", Time = "12.00", Day = "Tue" },
           cros,
           new Course { CourseID = 3, Name = "Test1", Description = "Test1", Section = "3", Credit = "1", Time = "12.00", Day = "Tue" },
});

            AdminController target = new AdminController(mock.Object);

            target.Delete(cros.CourseID);

            mock.Verify(m => m.DeleteCourse(cros.CourseID));
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {

            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();

            AdminController target = new AdminController(mock.Object);

            Course course = new Course { Name = "Test" };

            ActionResult result = target.Edit(course);

            mock.Verify(m => m.SaveCourse(course));

            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {

            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();

            AdminController target = new AdminController(mock.Object);

            Course course = new Course { Name = "Test" };

            target.ModelState.AddModelError("error", "error");

            ActionResult result = target.Edit(course);

            mock.Verify(m => m.SaveCourse(It.IsAny<Course>()), Times.Never());

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Edit_Course()
        {

            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new Course[] {
           new Course {CourseID=1,Name="Test1",Description="Test1",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=2,Name="Test2",Description="Test2",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=3,Name="Test3",Description="Test3",Section="3",Credit="1",Time="12.00",Day="Tue"}
});

            AdminController target = new AdminController(mock.Object);

            Course p1 = target.Edit(1).ViewData.Model as Course;
            Course p2 = target.Edit(2).ViewData.Model as Course;
            Course p3 = target.Edit(3).ViewData.Model as Course;

            Assert.AreEqual(1, p1.CourseID);
            Assert.AreEqual(2, p2.CourseID);
            Assert.AreEqual(3, p3.CourseID);
        }
        [TestMethod]
        public void Cannot_Edit_Nonexistent_Course()
        {

            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new Course[] {
           new Course {CourseID=1,Name="Test1",Description="Test1",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=2,Name="Test2",Description="Test2",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=3,Name="Test3",Description="Test3",Section="3",Credit="1",Time="12.00",Day="Tue"}
});

            AdminController target = new AdminController(mock.Object);

            Course result = (Course)target.Edit(4).ViewData.Model;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {

            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new Course[] {
           new Course {CourseID=1,Name="Test1",Description="Test1",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=2,Name="Test2",Description="Test2",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=3,Name="Test3",Description="Test3",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=4,Name="Test4",Description="Test4",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=5,Name="Test5",Description="Test5",Section="3",Credit="1",Time="12.00",Day="Tue"}
});

            CourseController controller = new CourseController(mock.Object);
            controller.PageSize = 3;

            CoursesListViewModel result = (CoursesListViewModel)controller.List(2).Model;

            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Paginate()
        {

            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new Course[] {
           new Course {CourseID=1,Name="Test1",Description="Test1",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=2,Name="Test2",Description="Test2",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=3,Name="Test3",Description="Test3",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=4,Name="Test4",Description="Test4",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=5,Name="Test5",Description="Test5",Section="3",Credit="1",Time="12.00",Day="Tue"}
});
            CourseController controller = new CourseController(mock.Object);
            controller.PageSize = 3;

            CoursesListViewModel result = (CoursesListViewModel)controller.List(2).Model;

            Course[] courseArray = result.Courses.ToArray();
            Assert.IsTrue(courseArray.Length == 2);
            Assert.AreEqual(courseArray[0].Name, "Test4");
            Assert.AreEqual(courseArray[1].Name, "Test5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
          
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
       + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
       + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
       result.ToString());
        }
    }
}