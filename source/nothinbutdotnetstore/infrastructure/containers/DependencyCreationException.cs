using System;

namespace nothinbutdotnetstore.infrastructure.containers
{
    public class DependencyCreationException: Exception
    {
    	public DependencyCreationException(Type type_that_could_not_be_created, Exception inner)
			: base("An exception was thrown by the factory", inner)
    	{
    		this.type_that_could_not_be_created = type_that_could_not_be_created;
    	}

    	public Type type_that_could_not_be_created { get; private set; }
    }
}