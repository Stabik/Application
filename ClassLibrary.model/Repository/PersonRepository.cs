using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.model.Repository
{
    [Repository]
    public class PersonRepository : Repository<Person>
    {
        public PersonRepository(ISession session)
            : base(session)
        {
        }

    }
}
