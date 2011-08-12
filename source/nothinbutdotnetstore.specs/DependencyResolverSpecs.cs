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

        public class when_fetching_a_dependency : concern
        {
            Establish c = () =>
            {
               
                the_single_dependency_factory = fake.an<ICreateASingleDependency>();
                the_item = new DependencyImplementor(the_single_dependency_factory);
                all_dependencies = new Dictionary<Type, ICreateASingleDependency>();
                all_dependencies[typeof(IDependencyToFetch)] = the_single_dependency_factory;

                depends.on(all_dependencies);

                the_single_dependency_factory.setup(x => x.create()).Return(the_item);
            };

            Because b = () =>
                result = sut.an<IDependencyToFetch>();

            It should_provide_an_instance_of_the_requested_depencency = () =>
                result.ShouldBeOfType(typeof(DependencyImplementor));

            static IDependencyToFetch result;
            static IDictionary<Type, ICreateASingleDependency> all_dependencies;
            static IDependencyToFetch the_item;
            static ICreateASingleDependency the_single_dependency_factory;
        }
    }

    class DependencyImplementor : IDependencyToFetch
    {
        ICreateASingleDependency factory;
        public DependencyImplementor(ICreateASingleDependency factory)
        {
            this.factory = factory;
        }

        public ICreateASingleDependency GetFactory()
        {
            return factory;
        }
    }

    interface IDependencyToFetch
    {
        ICreateASingleDependency GetFactory();
    }

    
}