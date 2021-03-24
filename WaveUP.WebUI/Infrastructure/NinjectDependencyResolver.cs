using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using WaveUP.Domain.Abstract;
using WaveUP.Domain.Entities;

namespace WaveUP.WebUI.Infrastructure
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
            // Здесь размещаются привязки
            Mock<IInstrumenteRepository> mock = new Mock<IInstrumentRepository>();
            mock.Setup(m => m.Instruments).Returns(new List<Instrument>
            {
                new Instrument { Name = "Guitar", Price = 1000 },
                new Instrument { Name = "Violin", Price=1550 },
                new Instrument { Name = "Piano", Price=10000 }
            });
            kernel.Bind<IInstrumentRepository>().ToConstant(mock.Object);
        }
    }
}