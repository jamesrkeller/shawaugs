using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.specs.utility;

namespace nothinbutdotnetstore.specs
{
	[Subject(typeof(GreedyConstructorSelectionStrategy))]  
	public class GreedyConstructorSelectionStrategySpecs
	{

		public abstract class concern : Observes<IPickTheConstructorToCreateTheItemWith, GreedyConstructorSelectionStrategy>
		{
		}

		public class when_starting_the_application : concern
		{
			Establish c = () =>
			{
				type = typeof(OurTypeWithDependencies);
				expected_ctor = ObjectFactory.expressions.to_target<OurTypeWithDependencies>().
					get_the_constructor_pointed_at_by(() => new OurTypeWithDependencies(null, null, null));
			};

			Because b = () =>
				the_constructor = sut.get_the_applicable_constructor_on(type);

			It should_return_the_item_created_by_the_provided_block = () =>
				the_constructor.ShouldEqual(expected_ctor);

			static object the_constructor;
			static Type type;
			static ConstructorInfo expected_ctor;
		}
	}
}