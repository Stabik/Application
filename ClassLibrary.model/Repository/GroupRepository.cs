using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.model.Repository
{
    [Repository]
    public class GroupRepository: Repository<Group>

    {
        public GroupRepository(ISession session)
            : base(session)
        {
        }
        
    }
}
