namespace nothinbutdotnetstore.web.core
{
    public class FrontController : IProcessWebRequests
    {
    	IFindCommandsThatCanProcessRequests registry;
		public FrontController(IFindCommandsThatCanProcessRequests registry)
		{
			this.registry = registry;
		}

        public void process(IContainRequestInformation the_request)
        {
        	registry.get_command_for(the_request).process(the_request);
        }
    }
}