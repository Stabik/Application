using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Web;

namespace AspNetMvc4Vs15.Models
{
    public class HibernateHelper
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurePath = HttpContext.Current.Server.MapPath(@"~\Models\hibernate.cfg.xml");
            configuration.Configure(configurePath);
            configuration.AddAssembly(typeof(Document).Assembly);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            new SchemaUpdate(configuration).Execute(true, true);
            return sessionFactory.OpenSession();
        }
    }
}