using System;
using System.Collections.Generic;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using nothinbutdotnetstore.infrastructure.containers;
using developwithpassion.specifications.extensions;

namespace nothinbutdotnetstore.specs
{
    [Subject(typeof(DependencyResolver))]
    public class DependencyResolverSpecs
    {
        public abstract class concern : Observes<IFetchDependencies, DependencyResolver>
        {
        }

        public class when_generating_a_view : concern
        {
            Establish c = () =>
            {
                registry = depends.on<IDictionary<Type, object>>();

                registry.setup(reg => reg[typeof(IDependencyToFetch)]).Return(new DependencyImplementor());
            };

            Because b = () =>
                result = sut.an<IDependencyToFetch>();

            It should_provide_an_instance_of_the_requested_depencency = () =>
                result.ShouldBeOfType(typeof(DependencyImplementor));

            static IDependencyToFetch result;
            static IDictionary<Type, object> registry;
        }
    }

    class DependencyImplementor : IDependencyToFetch
    {
    }

    interface IDependencyToFetch
    {
    }

    
}