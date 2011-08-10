using System.Collections.Generic;
using System.Linq;

namespace nothinbutdotnetstore.web.core
{
	public class CommandRegistry : IFindCommandsThatCanProcessRequests
	{
		IEnumerable<IProcessOneSpecificRequest> commands;
		public CommandRegistry(IEnumerable<IProcessOneSpecificRequest> commands)
		{
			this.commands = commands;
		}

		public IProcessOneSpecificRequest get_command_for(IContainRequestInformation the_request)
		{
			return commands.First(c => c.can_process(the_request));
		}
	}
}