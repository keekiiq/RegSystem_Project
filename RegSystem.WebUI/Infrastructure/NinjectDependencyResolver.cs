using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Ninject;
using RegSystem.Domain.Entities;
using RegSystem.Domain.Abstract;

namespace RegSystem.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new List<Course> {
                new Course { CourseID="953100", Name="Intro for SE 1",Description="Test",Section="701",Time="12.00-13.00",Day="Mon-Tue"},
                new Course { CourseID="953101", Name="Intro for SE 2",Description="Test",Section="701",Time="12.00-13.00",Day="Mon-Tue"},
                new Course { CourseID="953102", Name="Intro for SE 3",Description="Test",Section="701",Time="12.00-13.00",Day="Mon-Tue"}
            });
            kernel.Bind<ICourseRepository>().ToConstant(mock.Object);
        }
    }
}