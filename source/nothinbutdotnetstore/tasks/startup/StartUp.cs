using System;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.web.core;

namespace nothinbutdotnetstore.tasks.startup
{
    public class StartUp
    {
        static Dictionary<Type, ICreateASingleDependency> dependencies;
        static DependencyResolver the_container;

        public static void run()
        {
            configure_core_facilities();

            configure_the_front_controller();
        }

        static void configure_the_front_controller()
        {
//            dependencies.Add(typeof(IProcessWebRequests),new AutomaticDependencyFactory(typeof(FrontController),
//                the_container,));
        }

        static void configure_core_facilities()
        {
            dependencies = new Dictionary<Type, ICreateASingleDependency>();
            the_container = new DependencyResolver(dependencies);
            ContainerFacadeResolver resolver = () => the_container;
            Container.facade_resolver = resolver;
        }
    }
}