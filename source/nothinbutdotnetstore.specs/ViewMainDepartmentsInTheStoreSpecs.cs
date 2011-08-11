 using System;
 using System.Collections.Generic;
 using System.Linq;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using nothinbutdotnetstore.web.application.catalogbrowsing;
 using nothinbutdotnetstore.web.core;

namespace nothinbutdotnetstore.specs
{  
    [Subject(typeof(ViewMainDepartmentsInTheStore))]  
    public class ViewMainDepartmentsInTheStoreSpecs
    {
        public abstract class concern : Observes<IProcessAnApplicationBehaviour, ViewMainDepartmentsInTheStore>
        {
			
        }

   
        public class when_processing_a_request : concern
        {
        	Establish c = () =>
        	{
				depends.on<Func<IEnumerable<Department>>>(() => Enumerable.Range(1, 10).Select(x => new Department()));
        		depends.on<Action<IEnumerable<Department>>>(departments => view_data = departments);

        		request = fake.an<IContainRequestInformation>();
        	};

        	Because b = () =>
        		sut.process(request);

        	It should_provide_departments_to_view = () =>
        		view_data.ShouldNotBeEmpty();

        	static IContainRequestInformation request;
        	static IEnumerable<Department> view_data;
        }
    }
}
