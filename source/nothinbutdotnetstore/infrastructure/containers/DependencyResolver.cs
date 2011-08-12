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
        	ICreateASingleDependency factory;
			if (!factories.TryGetValue(typeof(Dependency), out factory))
				throw new DependencyFactoryNotRegisteredException(typeof(Dependency));
			try
			{
				return (Dependency) factory.create();
			}
			catch(Exception ex)
			{
				throw new DependencyCreationException(typeof(Dependency), ex);
			}
        }
    }
}