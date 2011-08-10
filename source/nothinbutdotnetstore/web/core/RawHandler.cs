using System.Web;

namespace nothinbutdotnetstore.web.core
{
    public class RawHandler : IHttpHandler
    {
		IProcessWebRequests processor;
    	ICreateRequestsTheFrontControllerCanProcess mapper;

    	public RawHandler(IProcessWebRequests processor, ICreateRequestsTheFrontControllerCanProcess mapper)
    	{
    		this.processor = processor;
    		this.mapper = mapper;
    	}

    	public void ProcessRequest(HttpContext context)
        {
			processor.process(mapper.map_from(context));
        }

        public bool IsReusable
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}