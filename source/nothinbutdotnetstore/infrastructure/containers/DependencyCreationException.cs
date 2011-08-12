using System;

namespace nothinbutdotnetstore.infrastructure.containers
{
    public class DependencyCreationException: Exception
    {
        public DependencyCreationException(Type type, Exception innerException):base(string.Empty, innerException)
        {
            type_that_could_not_be_created = type;
             
        }
        public Type type_that_could_not_be_created { get; private set; }
    }
}