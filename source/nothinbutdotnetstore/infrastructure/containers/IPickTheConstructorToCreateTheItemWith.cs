using System;
using System.Reflection;

namespace nothinbutdotnetstore.infrastructure.containers
{
    public interface IPickTheConstructorToCreateTheItemWith
    {
        ConstructorInfo get_the_applicable_constructor_on(Type type);
    }
}