using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.model.Repository
{
    [Repository]
    public class RightAccessRepository : Repository<RightAccess>
    
    {
        public RightAccessRepository(ISession session)
            : base(session)
        {
        }
    }
}
