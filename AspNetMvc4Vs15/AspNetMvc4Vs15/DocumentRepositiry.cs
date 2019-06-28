
using AspNetMvc4Vs15.Models;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using System;
using System.Collections.Generic;



namespace AspNetMvc4Vs15
{
    public class DocumentRepositiry

    {
        public MyFile GetFileBy(int id)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {

                var result = session.QueryOver<MyFile>().Where(x => x.Id == id).SingleOrDefault();

                return result ?? new MyFile();

            }
        }

        public Document GetDocumentBy(int? id)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {

                var result = session.QueryOver<Document>().Where(x => x.Id == id).SingleOrDefault();
                return result ?? new Document();
            }
        }
        public List<Document> GetDocumentBy(string serching)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                
                    var criteria = session.CreateCriteria<Document>();
                    criteria.CreateAlias("MyUser", "Person", JoinType.LeftOuterJoin);
                    criteria.SetResultTransformer(new DistinctRootEntityResultTransformer());

                    List<Document> list = new List<Document>();
                    foreach (var i in criteria.List<Document>())
                    {
                        if (i.Name.Contains(serching))
                        { list.Add(i); }
                    }
                    return list;
                
                
            }
        }
        public List<Document> GetDocumentByAuthor(string serching)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                
                    var criteria = session.CreateCriteria<Document>();
                    criteria.CreateAlias("MyUser", "Person", JoinType.LeftOuterJoin);
                    criteria.SetResultTransformer(new DistinctRootEntityResultTransformer());

                    List<Document> list = new List<Document>();
                    foreach (var i in criteria.List<Document>())
                    {
                        if (i.MyUser.Name.Contains(serching))
                        { list.Add(i); }
                    }
                    return list;
                  
               
            }
        }
        public List<Document> GetDocumentByDate(DateTime? startDate, DateTime? endDate)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                {
                    var criteria = session.CreateCriteria<Document>();
                    criteria.CreateAlias("MyUser", "Person", JoinType.LeftOuterJoin);
                    criteria.SetResultTransformer(new DistinctRootEntityResultTransformer());

                    List<Document> list = new List<Document>();
                    foreach (var i in criteria.List<Document>())
                    {
                        if (i.Date >= startDate && i.Date <= endDate)
                        { list.Add(i); }
                    }
                    return list;
                  
                }
            }
        }

        public List<Document> Doclist()
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                var criteria = session.CreateCriteria<Document>();
                criteria.CreateAlias("MyUser", "Person", JoinType.LeftOuterJoin);
                criteria.SetResultTransformer(new DistinctRootEntityResultTransformer());
                
                List<Document> list = new List<Document>();
                foreach (var i in criteria.List<Document>())
                {
                    list.Add(i);
                }
                return list;
              
            }
        }
       
    }
}