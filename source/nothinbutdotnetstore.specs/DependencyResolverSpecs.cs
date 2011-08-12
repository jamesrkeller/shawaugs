using System;
using System.Collections.Generic;
using Machine.Specifications;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using nothinbutdotnetstore.infrastructure.containers;

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
            public class and_it_has_the_factory_for_the_dependency : when_fetching_a_dependency
            {
                Establish c = () =>
                {
                    the_item = new DependencyImplementor();
                    the_single_dependency_factory = fake.an<ICreateASingleDependency>();
                    all_dependencies = new Dictionary<Type, ICreateASingleDependency>();
                    all_dependencies[typeof(IDependencyToFetch)] = the_single_dependency_factory;

                    depends.on(all_dependencies);

                    the_single_dependency_factory.setup(x => x.create()).Return(the_item);
                };

                Because b = () =>
                    result = sut.an<IDependencyToFetch>();

                It should_provide_an_instance_of_the_requested_depencency = () =>
                    result.ShouldEqual(the_item);

                static IDependencyToFetch result;
                static IDictionary<Type, ICreateASingleDependency> all_dependencies;
                static IDependencyToFetch the_item;
                static ICreateASingleDependency the_single_dependency_factory;
            }

            public class and_it_does_not_have_the_factory_for_the_dependency : when_fetching_a_dependency
            {
                Establish c = () =>
                {
                    the_item = new DependencyImplementor();
                    all_dependencies = new Dictionary<Type, ICreateASingleDependency>();

                    depends.on(all_dependencies);
                };

                Because b = () =>
                    spec.catch_exception(() => sut.an<IDependencyToFetch>());

                It should_throw_a_dependency_factory_not_registered_exception_with_access_to_the_necessary_information = () =>
                {
                    var exception = spec.exception_thrown.ShouldBeAn<DependencyFactoryNotRegisteredException>();
                    exception.type_that_had_no_factory.ShouldEqual(typeof(IDependencyToFetch));
                };

                static IDependencyToFetch result;
                static IDictionary<Type, ICreateASingleDependency> all_dependencies;
                static IDependencyToFetch the_item;
            }

            public class and_the_dependency_factory_for_a_dependency_throws_an_exception_while_creating_its_dependency : when_fetching_a_dependency
            {
                Establish c = () =>
                {
                    the_item = new DependencyImplementor();
                    the_single_dependency_factory = fake.an<ICreateASingleDependency>();
                    all_dependencies = new Dictionary<Type, ICreateASingleDependency>();
                    all_dependencies[typeof(IDependencyToFetch)] = the_single_dependency_factory;
                    inner_exception =  new Exception();

                    depends.on(all_dependencies);

                    the_single_dependency_factory.setup(x => x.create()).Throw(inner_exception);
                };

                Because b = () =>
                    spec.catch_exception(() => sut.an<IDependencyToFetch>());

                It should_throw_a_dependency_creation_exception_with_access_to_the_correct_information   = () =>
                {
                    var exception = spec.exception_thrown.ShouldBeAn<DependencyCreationException>();
                    exception.type_that_could_not_be_created.ShouldEqual(typeof(IDependencyToFetch));
                    exception.InnerException.ShouldEqual(inner_exception);
                };

                static IDependencyToFetch result;
                static Dictionary<Type, ICreateASingleDependency> all_dependencies;
                static IDependencyToFetch the_item;
                static ICreateASingleDependency the_single_dependency_factory;
                static Exception inner_exception;
            }
        }

        class DependencyImplementor : IDependencyToFetch
        {
        }

        interface IDependencyToFetch
        {
        }
    }
}