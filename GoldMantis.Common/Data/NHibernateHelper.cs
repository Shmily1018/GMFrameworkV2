using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.RegularExpressions;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Hql.Ast;
using NHibernate.Linq;
using NHibernate.Linq.Functions;
using NHibernate.Linq.Visitors;

namespace GoldMantis.Common
{
    public sealed class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static Configuration NHConguration { get; set; }

        public static void BindSession()
        {
            if (NHConguration != null)
            {
                var session = NHibernateHelper.SessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }
        }

        public static void UnBindSession()
        {
            if (NHConguration != null)
            {
                var session = CurrentSessionContext.Unbind(NHibernateHelper.SessionFactory);
                session.Dispose();
            }
        }

        public static void Configure()
        {
            if (NHConguration == null)
            {
                NHConguration = new Configuration().Configure();
                NHConguration.LinqToHqlGeneratorsRegistry<NHibernateLinqToHqlGeneratorsRegistry>();

                if (_sessionFactory == null)
                {
                    _sessionFactory = NHConguration.BuildSessionFactory();
                }
            }
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = NHConguration.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public static void CloseSessionFactory()
        {
            if (_sessionFactory != null)
            {
                _sessionFactory.Close();
            }
        }
    }


    public class NHibernateLinqToHqlGeneratorsRegistry : DefaultLinqToHqlGeneratorsRegistry
    {
        public NHibernateLinqToHqlGeneratorsRegistry()
        {
            RegisterGenerator(NHibernate.Linq.ReflectionHelper.GetMethodDefinition(() => NHibernateLinqExtensions.IsLike(null, null)), new IsLikeGenerator());
        }
    }

    public static class NHibernateLinqExtensions
    {
        public static bool IsLike(this string source, string pattern)
        {
            pattern = Regex.Escape(pattern);
            pattern = pattern.Replace("%", ".*?").Replace("_", ".");
            pattern = pattern.Replace(@"\[", "[").Replace(@"\]", "]").Replace(@"\^", "^");

            return Regex.IsMatch(source, pattern);
        }
    }

    public class IsLikeGenerator : BaseHqlGeneratorForMethod
    {
        public IsLikeGenerator()
        {
            SupportedMethods = new[] { NHibernate.Linq.ReflectionHelper.GetMethodDefinition(() => NHibernateLinqExtensions.IsLike(null, null)) };
        }

        public override HqlTreeNode BuildHql(MethodInfo method, System.Linq.Expressions.Expression targetObject, 
            ReadOnlyCollection<System.Linq.Expressions.Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return treeBuilder.Like(visitor.Visit(arguments[0]).AsExpression(), visitor.Visit(arguments[1]).AsExpression());
        }
    }
}