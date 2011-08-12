using System;

namespace nothinbutdotnetstore.infrastructure.containers
{
    public class DependencyFactoryNotRegisteredException:Exception
    {
        public Type type_that_had_no_factory { get; private set; }
    }
}