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
            public void Can_Login_With_Valid_Credentials()
            {
                // Arrange - create a mock authentication provider
                Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
                mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
                // Arrange - create the view model
                LoginViewModel model = new LoginViewModel
                {
                    UserName = "admin",
                    Password = "secret"
                };
                // Arrange - create the controller
                AccountController target = new AccountController(mock.Object);
                // Act - authenticate using valid credentials
                ActionResult result = target.Login(model, "/MyURL");
                // Assert
                Assert.IsInstanceOfType(result, typeof(RedirectResult));
                Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
            }
            [TestMethod]
            public void Cannot_Login_With_Invalid_Credentials()
            {
                // Arrange - create a mock authentication provider
                Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
                mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);
                // Arrange - create the view model
                LoginViewModel model = new LoginViewModel
                {
                    UserName = "badUser",
                    Password = "badPass"
                };
                // Arrange - create the controller
                AccountController target = new AccountController(mock.Object);
                // Act - authenticate using valid credentials
                ActionResult result = target.Login(model, "/MyURL");
                // Assert
                Assert.IsInstanceOfType(result, typeof(ViewResult));
                Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
            }

    [TestMethod]
        public void Can_Delete_Valid_Courses()
        {
            // Arrange - create a Product
            Course cros = new Course { CourseID = 2, Name = "Test1", Description = "Test1", Section = "3", Credit = "1", Time = "12.00", Day = "Tue" };
            // Arrange - create the mock repository
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new Course[] {
           new Course { CourseID = 1, Name = "Test1", Description = "Test1", Section = "3", Credit = "1", Time = "12.00", Day = "Tue" },
           cros,
           new Course { CourseID = 3, Name = "Test1", Description = "Test1", Section = "3", Credit = "1", Time = "12.00", Day = "Tue" },
});
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act - delete the product
            target.Delete(cros.CourseID);
            // Assert - ensure that the repository delete method was
            // called with the correct Product
            mock.Verify(m => m.DeleteCourse(cros.CourseID));
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create mock repository
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a product
            Course course = new Course { Name = "Test" };
            // Act - try to save the product
            ActionResult result = target.Edit(course);
            // Assert - check that the repository was called
            mock.Verify(m => m.SaveCourse(course));
            // Assert - check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - create mock repository
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a product
            Course course = new Course { Name = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");
            // Act - try to save the product
            ActionResult result = target.Edit(course);
            // Assert - check that the repository was not called
            mock.Verify(m => m.SaveCourse(It.IsAny<Course>()), Times.Never());
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Edit_Course()
        {
            // Arrange - create the mock repository
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new Course[] {
           new Course {CourseID=1,Name="Test1",Description="Test1",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=2,Name="Test2",Description="Test2",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=3,Name="Test3",Description="Test3",Section="3",Credit="1",Time="12.00",Day="Tue"}
});
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act
            Course p1 = target.Edit(1).ViewData.Model as Course;
            Course p2 = target.Edit(2).ViewData.Model as Course;
            Course p3 = target.Edit(3).ViewData.Model as Course;
            // Assert
            Assert.AreEqual(1, p1.CourseID);
            Assert.AreEqual(2, p2.CourseID);
            Assert.AreEqual(3, p3.CourseID);
        }
        [TestMethod]
        public void Cannot_Edit_Nonexistent_Course()
        {
            // Arrange - create the mock repository
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new Course[] {
           new Course {CourseID=1,Name="Test1",Description="Test1",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=2,Name="Test2",Description="Test2",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=3,Name="Test3",Description="Test3",Section="3",Credit="1",Time="12.00",Day="Tue"}
});
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act
            Course result = (Course)target.Edit(4).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new Course[] {
           new Course {CourseID=1,Name="Test1",Description="Test1",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=2,Name="Test2",Description="Test2",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=3,Name="Test3",Description="Test3",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=4,Name="Test4",Description="Test4",Section="3",Credit="1",Time="12.00",Day="Tue"},
           new Course {CourseID=5,Name="Test5",Description="Test5",Section="3",Credit="1",Time="12.00",Day="Tue"}
});
            // Arrange
            CourseController controller = new CourseController(mock.Object);
            controller.PageSize = 3;
            // Act
            CoursesListViewModel result = (CoursesListViewModel)controller.List(2).Model;
            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
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
            // Act
            CoursesListViewModel result = (CoursesListViewModel)controller.List(2).Model;
            // Assert
            Course[] prodArray = result.Courses.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "Test4"); Assert.AreEqual(prodArray[1].Name, "Test5");
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