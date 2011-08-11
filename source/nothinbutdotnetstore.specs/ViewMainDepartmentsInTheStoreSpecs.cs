using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using nothinbutdotnetstore.web.application;
using nothinbutdotnetstore.web.application.catalogbrowsing;
using nothinbutdotnetstore.web.core;

namespace nothinbutdotnetstore.specs
{
    [Subject(typeof(ViewMainDepartmentsInTheStore))]
    public class ViewMainDepartmentsInTheStoreSpecs
    {
        public abstract class concern : Observes<IProcessAnApplicationBehaviour,
                                            ViewMainDepartmentsInTheStore>
        {
        }

        public class when_processing_a_request : concern
        {
            Establish c = () =>
            {
                department_repository = depends.on<IReturnDepartments>();
                request = fake.an<IContainRequestInformation>();
            };

            Because b = () =>
                sut.process(request);

            It should_ask_the_department_repository_for_the_main_departments = () =>
                department_repository.received(x => x.get_the_main_departments_in_the_store());

            static IContainRequestInformation request;
            static IReturnDepartments department_repository;
        }

		public class when_providing_view_with_data : concern
		{
			Establish c = () =>
			{
				department_repository = depends.on<IReturnDepartments>();
				depends.on<Action<object>>(x => view_data = x);
				request = fake.an<IContainRequestInformation>();

				department_repository.setup(x => x.get_the_main_departments_in_the_store()).Return(() => Enumerable.Range(1, 10).Select(x => new Department()));
			};

			Because b = () =>
				sut.process(request);

			It should_send_departments_to_view = () =>
				view_data.ShouldBeAn<IEnumerable<Department>>().Count().ShouldEqual(10);

			static IContainRequestInformation request;
			static IReturnDepartments department_repository;
			static object view_data;
		}
    }
}