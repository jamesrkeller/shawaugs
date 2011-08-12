using System;
using System.Linq;

namespace nothinbutdotnetstore.infrastructure.containers
{
    public class AutomaticDependencyFactory :ICreateASingleDependency
    {
        Type type_to_create;
        IFetchDependencies container;
        IPickTheConstructorToCreateTheItemWith constructor_picker;

        public AutomaticDependencyFactory(Type type_to_create, IFetchDependencies container, IPickTheConstructorToCreateTheItemWith constructor_picker)
        {
            this.type_to_create = type_to_create;
            this.container = container;
            this.constructor_picker = constructor_picker;
        }

        public object create()
        {
            var ctor = constructor_picker.get_the_applicable_constructor_on(type_to_create);
            var ctor_args = ctor.GetParameters().Select(x => container.an(x.ParameterType)).ToArray();
            return ctor.Invoke(ctor_args);
        }
    }
}