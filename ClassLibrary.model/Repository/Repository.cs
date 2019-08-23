using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.model.Repository
{
    
        public class Repository<T>
         where T : class
        {
            protected ISession session;

            public Repository(ISession session)
            {
                this.session = session;
            }

            public virtual T Load(int id)
            {
                return session.Get<T>(id);
            }


            public virtual void Save(T entity)
            {
            using (var tran = session.BeginTransaction())
            {
                session.Save(entity);
                tran.Commit();
            }
           
               
            }
        }
    
}
