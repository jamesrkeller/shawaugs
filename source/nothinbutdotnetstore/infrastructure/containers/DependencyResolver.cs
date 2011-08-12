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
            return (Dependency) an(typeof(Dependency)); 
        }

        public object an(Type type)
        {
            ensure_there_is_a_factory_for(type);
            try
            {
                return factories[type].create();
            }
            catch(Exception e)
            {
                throw new DependencyCreationException(type,e);
            }

        }

        void ensure_there_is_a_factory_for(Type type)
        {
            if (factories.ContainsKey(type)) return;
            throw new DependencyFactoryNotRegisteredException(type);
        }
    }
}