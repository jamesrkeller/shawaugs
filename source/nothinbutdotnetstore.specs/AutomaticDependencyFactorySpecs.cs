using System.Data;
using System.Reflection;
using Machine.Specifications;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.specs.utility;

namespace nothinbutdotnetstore.specs
{
    [Subject(typeof(AutomaticDependencyFactory))]
    public class AutomaticDependencyFactorySpecs
    {
        public abstract class concern : Observes<ICreateASingleDependency,
                                            AutomaticDependencyFactory>
        {
        }

        public class when_creating_a_type_that_has_depedencies : concern
        {
            Establish c = () =>
            {
                container = depends.on<IFetchDependencies>();
                constructor_selector = depends.on<IPickTheConstructorToCreateTheItemWith>();
                depends.on(typeof(OurTypeWithDependencies));

                ctor = ObjectFactory.expressions.to_target<OurTypeWithDependencies>().
                    get_the_constructor_pointed_at_by(() => new OurTypeWithDependencies(null, null, null));


                the_connection = fake.an<IDbConnection>();
                the_command = fake.an<IDbCommand>();
                the_reader = fake.an<IDataReader>();
                container.setup(x => x.an(typeof(IDbConnection))).Return(the_connection);
                container.setup(x => x.an(typeof(IDbCommand))).Return(the_command);
                container.setup(x => x.an(typeof(IDataReader))).Return(the_reader);

                constructor_selector.setup(x => x.get_the_applicable_constructor_on(typeof(OurTypeWithDependencies)))
                    .Return(ctor);

            };

            Because b = () =>
                result = sut.create();

            It should_return_the_instance_with_all_of_its_dependencies_satisfied = () =>
            {
                var item = result.ShouldBeAn<OurTypeWithDependencies>();
                item.connection.ShouldEqual(the_connection);
                item.command.ShouldEqual(the_command);
                item.reader.ShouldEqual(the_reader);
            };

            static object result;
            static IDbConnection the_connection;
            static IDbCommand the_command;
            static IDataReader the_reader;
            static IPickTheConstructorToCreateTheItemWith constructor_selector;
            static ConstructorInfo ctor;
            static IFetchDependencies container;
        }
    }

	public class OurTypeWithDependencies
	{
		public IDbConnection connection;
		public IDbCommand command;
		public IDataReader reader;

		public OurTypeWithDependencies(IDbConnection connection, IDbCommand command, IDataReader reader)
		{
			this.connection = connection;
			this.command = command;
			this.reader = reader;
		}

		public OurTypeWithDependencies(IDbConnection connection, IDbCommand command)
		{
			this.connection = connection;
			this.command = command;
		}

		public OurTypeWithDependencies(IDbConnection connection)
		{
			this.connection = connection;
		}
	}
}