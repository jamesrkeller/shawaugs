using System;

namespace nothinbutdotnetstore.infrastructure.containers
{
    public class ExplicitDependencyFactory : ICreateASingleDependency
    {
        Func<object> factory;

        public ExplicitDependencyFactory(Func<object> factory)
        {
            this.factory = factory;
        }

        public object create()
        {
            return factory();
        }
    }
}