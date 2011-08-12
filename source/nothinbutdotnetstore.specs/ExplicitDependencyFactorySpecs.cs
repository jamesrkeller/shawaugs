 using System;
 using System.Data;
 using System.Data.SqlClient;
 using Machine.Specifications;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;
 using nothinbutdotnetstore.infrastructure.containers;

namespace nothinbutdotnetstore.specs
{  
    [Subject(typeof(ExplicitDependencyFactory))]  
    public class ExplicitDependencyFactorySpecs
    {
        public abstract class concern : Observes<ICreateASingleDependency,
                                            ExplicitDependencyFactory>
        {
        
        }

   
        public class when_creating_a_dependency : concern
        {
            Establish c = () =>
            {
                the_connection = new SqlConnection();
                depends.on<Func<object>>(() => the_connection);
            };

            Because b = () =>
                result = sut.create();

            It should_return_the_item_created_by_the_provided_block = () =>
                result.ShouldEqual(the_connection);

            static object result;
            static IDbConnection the_connection;
        }
    }
}
