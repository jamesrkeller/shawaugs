using System;
using System.Collections.Generic;
using System.Linq;

namespace nothinbutdotnetstore.web.core
{
	public class CommandRegistry : IFindCommandsThatCanProcessRequests
	{
		IEnumerable<IProcessOneSpecificRequest> commands;
		MissingCommandFactory command_missing;
		public CommandRegistry(IEnumerable<IProcessOneSpecificRequest> commands, MissingCommandFactory command_missing)
		{
			this.commands = commands;
			this.command_missing = command_missing;
		}

		public IProcessOneSpecificRequest get_command_for(IContainRequestInformation the_request)
		{
			return commands.FirstOr<IProcessOneSpecificRequest>(c => c.can_process(the_request), command_missing);
		}
	}

	public static class EnumerableExtensions
	{
		public static T FirstOr<T>(this IEnumerable<T> items, Func<T, bool> predicate, Func<T> createDefault)
			where T : class 
		{
			return items.FirstOrDefault(predicate)
				?? createDefault();
		}
	}
}