
using AspNetMvc4Vs15.Models;
using NHibernate;
using System;
using System.Collections.Generic;



namespace AspNetMvc4Vs15
{
    public class DocumentRepositiry

    {
        
        public void Add(Document doc)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                  
                    try
                    {
                        doc.Date = DateTime.Now;
                        session.Save(doc);
                        transaction.Commit();
                    }
                    catch { }// сделать сообщение
                }
            }
        }
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
                {
                    var res = session.CreateQuery("select c from Document c where c.Id >0");
                    List<Document> list = new List<Document>();
                    foreach (var i in res.List<Document>())
                    {
                        if (i.Name.Contains(serching))
                        {
                            list.Add(i);
                        }
                    }
                    return list;
                }
            }
        }
        public List<Document> GetDocumentByAuthor(string serching)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                {
                    var res = session.CreateQuery("select c from Document c where c.Id >0");
                    List<Document> list = new List<Document>();
                    foreach (var i in res.List<Document>())
                    {
                        if (i.Author.Contains(serching))
                        {
                            list.Add(i);
                        }
                    }
                    return list;
                }
            }
        }
        public List<Document> GetDocumentByDate(DateTime? startDate, DateTime? endDate)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                {
                        var res = session.CreateQuery("select c from Document c where c.Id >0");
                        List<Document> list = new List<Document>();
                   
                        foreach (var i in res.List<Document>())
                        {
                            if (i.Date >= startDate && i.Date <= endDate)
                            {
                                list.Add(i);
                            }
                        }
                    return list;
                }
            }
        }
      
        public List<Document> Doclist()
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                var res = session.CreateQuery("select c from Document c where c.Id >0");
                List<Document> list = new List<Document>();
                foreach (var i in res.List<Document>())
                {                    
                    list.Add(i);
                }
                return list;
            }
        }
        public void Delete(int id)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                   
                    try
                    {
                        var result = session.QueryOver<Document>().Where(x => x.Id == id).SingleOrDefault();

                        session.Delete(result);
                        transaction.Commit();
                    }
                    catch { }// сделать сообщение
                }
            }
        }
    }
}