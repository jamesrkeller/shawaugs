using System;
using System.Collections.Generic;

namespace nothinbutdotnetstore.infrastructure.containers
{
    public class DependencyResolver : IFetchDependencies
    {
        IDictionary<Type, ICreateASingleDependency> factories;

        public DependencyResolver(IDictionary<Type, ICreateASingleDependency> factories)
        {
            this.factories = factories;
        }

        public Dependency an<Dependency>()
        {
            return (Dependency) factories[typeof(Dependency)].create();
        }
    }
}