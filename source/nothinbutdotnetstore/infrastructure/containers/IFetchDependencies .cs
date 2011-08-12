using System;

namespace nothinbutdotnetstore.infrastructure.containers
{
    public interface IFetchDependencies
    {
        Dependency an<Dependency>();
        object an(Type type);
    }
}