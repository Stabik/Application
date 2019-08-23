using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.model.Repository
{
    [Repository]
    public class DocumentReposirory : Repository<Group>

    {
        public DocumentReposirory(ISession session)
            : base(session)
        {
        }
        
    }
}
