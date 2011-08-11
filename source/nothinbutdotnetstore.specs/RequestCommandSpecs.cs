 using System;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using nothinbutdotnetstore.web.core;

namespace nothinbutdotnetstore.specs
{  
    [Subject(typeof(RequestCommand))]  
    public class RequestCommandSpecs
    {
        public abstract class concern : Observes<IProcessOneSpecificRequest, RequestCommand>
        {
        
        }

   
        public class when_determining_whether_it_can_handle_a_request : concern
        {
            Establish c = () =>
            {
            	depends.on<Predicate<IContainRequestInformation>>(x => true);
            	request = fake.an<IContainRequestInformation>();
            };

        	Because b = () =>
        		result = sut.can_process(request);
			
        	It should_delegate_the_processing_to_the_command_that_can_process_the_request = () =>
        		result.ShouldBeTrue();

            static IContainRequestInformation request;
        	static bool result;
        }
    }
}
