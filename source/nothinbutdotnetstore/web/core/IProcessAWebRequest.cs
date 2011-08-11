using System;

namespace nothinbutdotnetstore.web.core
{
    public interface IProcessOneSpecificRequest 
    {
        void process(IContainRequestInformation the_request);
        bool can_process(IContainRequestInformation request);
    }

	public class RequestCommand : IProcessOneSpecificRequest
	{
		Predicate<IContainRequestInformation> can_handle;
		public RequestCommand(Predicate<IContainRequestInformation> can_handle)
		{
			this.can_handle = can_handle;
		}

		public bool can_process(IContainRequestInformation request)
		{
			return can_handle(request);
		}

		public void process(IContainRequestInformation the_request)
		{
			throw new NotImplementedException();
		}
	}
}