using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using developwithpassion.specifications.extensions;

namespace nothinbutdotnetstore.specs.utility
{
    public class ObjectFactory
    {
        public static HttpContext create_http_context()
        {
            return new HttpContext(create_request(), create_response());
        }

        static HttpRequest create_request()
        {
            return new HttpRequest("blah.aspx", "http://sdfsfsdf/blah.aspx", String.Empty);
        }

        static HttpResponse create_response()
        {
            return new HttpResponse(new StringWriter());
        }

        public class expressions
        {
            public static ExpressionBuilder<T> to_target<T>()
            {
                return new ExpressionBuilder<T>();
            }
        }

        public class ExpressionBuilder<ItemToTarget>
        {
            public ConstructorInfo get_the_constructor_pointed_at_by(Expression<Func<ItemToTarget>> ctor)
            {
                return ctor.Body.downcast_to<NewExpression>().Constructor;
            }
        }
    }
}