using System;
using System.Data;
using System.Linq;
using System.Reflection;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using nothinbutdotnetstore.infrastructure.containers;

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
			};

			Because b = () =>
				the_constructor = sut.get_the_applicable_constructor_on(type);

			It should_return_the_item_created_by_the_provided_block = () =>
				the_constructor.ShouldEqual(type.GetConstructors().OrderBy(c => c.GetParameters().Count()).Last());

			static object the_constructor;
			static Type type;
		}
	}

	public class GreedyConstructorSelectionStrategy : IPickTheConstructorToCreateTheItemWith
	{
		public ConstructorInfo get_the_applicable_constructor_on(Type type)
		{
			return type.GetConstructors().OrderBy(c => c.GetParameters().Count()).Last();
		}
	}
}