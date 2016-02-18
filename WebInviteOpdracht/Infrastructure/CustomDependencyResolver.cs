using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebInviteOpdracht.Models;
using Ninject;

namespace WebInviteOpdracht.Infrastructure {
    public class CustomDependencyResolver : IDependencyResolver {
        private IKernel kernel;

        public CustomDependencyResolver(IKernel kernelParam) {
            kernel = kernelParam;
            AddBindings();
        }


        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings() {
            //Set Scope to inSingletonScope. 
            //This tells Ninject to create an IRepository implementation once, and use that for every request.
            //We can nopw be sure that the list with responses is the same on all requests.
            kernel.Bind<IRepository>().To<GuestResponseRepository>().InSingletonScope();
        }
    }
}