using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AspNetMvc4Vs15.Models;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Transform;

namespace AspNetMvc4Vs15.Controllers
{
    public class HomeController : Controller
    {
        public FileContentResult Download(int id)
        {
            var repository = new DocumentRepositiry();
            var result = repository.GetFileBy(id);
            string contentType = "application/octet-stream";
            return File(result.MyDocFile, contentType);
        }
        public string UserLog()
        { string s = null;
            if (User.Identity.IsAuthenticated)
            {
                s= User.Identity.Name ;
            }
            return s;
        }
        public ActionResult UserLogin()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Message ="Добро пожаловать, "+User.Identity.Name+" !";
            }
            return PartialView(ViewBag.Message);
        }
     
        [Authorize]
        public ActionResult Index(string serching, string Author)

        {
                UserLogin();
                var repository = new DocumentRepositiry();
                List<Document> result = new List<Document>();
                if (string.IsNullOrEmpty(serching) && string.IsNullOrEmpty(Author))
                {
                    result = repository.Doclist();
                }
                else if (!string.IsNullOrEmpty(serching))
                {
                    result = repository.GetDocumentBy(serching);
                }
                else if (!string.IsNullOrEmpty(Author))
                {
                    result = repository.GetDocumentByAuthor(Author);

                }
                return View(result);
           

        }
        [Authorize]
        [HttpPost]
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            
                var repository = new DocumentRepositiry();
                List<Document> result = new List<Document>();
                if (startDate < DateTime.MinValue && endDate < DateTime.MinValue)
                {
                    result = repository.Doclist();
                }
                result = repository.GetDocumentByDate(startDate, endDate);
                return View(result);
           
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Document doc, HttpPostedFileBase file1)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    if (doc.Name == null || file1 == null)
                    {
                        return RedirectToAction("_Error");
                    }

                    var find = session.QueryOver<Document>().Where(x => x.Name == doc.Name).SingleOrDefault();

                    if (find == null)
                    {
                        if (file1 != null)
                        {
                            doc.MyDoc = new MyFile();
                            doc.MyDoc.MyDocName = file1.FileName;
                            doc.MyDoc.MyDocFile = new byte[file1.ContentLength];
                            file1.InputStream.Read(doc.MyDoc.MyDocFile, 0, file1.ContentLength);

                        }
                        doc.Date = DateTime.Now;
                        doc.MyUser = new Person();
                        Person result = session.QueryOver<Person>().Where(x => x.Name == User.Identity.Name).SingleOrDefault();

                        doc.MyUser = result;

                        session.SaveOrUpdate(doc);
                        transaction.Commit();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Имя Документа в базе данных уже существует.";
                    }

                    return View();

                }

            }
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var repository = new DocumentRepositiry();
            var res = repository.GetDocumentBy(id);
            if (res != null)
            {
                return View(res);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Edit(Document doc,HttpPostedFileBase file1)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var find = session.QueryOver<Document>().Where(x => x.Name == doc.Name).SingleOrDefault();

                    if (find == null)
                    {

                        var criteria = session.CreateCriteria<Document>();
                        criteria.CreateAlias("MyUser", "Person", JoinType.LeftOuterJoin);
                        criteria.SetResultTransformer(new DistinctRootEntityResultTransformer());
                        List<Document> list = new List<Document>();
                        foreach (var i in criteria.List<Document>())
                        {
                            list.Add(i);
                        }
                        Document _doc = new Document();
                        foreach (var v in list)
                        {
                            if (v.Id == doc.Id)
                            {
                                _doc = v;
                            }
                        }
                        doc.Date = DateTime.Now;
                        _doc.Date = doc.Date;
                        _doc.Name = doc.Name;

                        if (file1 != null)
                        {
                            _doc.MyDoc.MyDocName = file1.FileName;
                            _doc.MyDoc.MyDocFile = new byte[file1.ContentLength];
                            file1.InputStream.Read(_doc.MyDoc.MyDocFile, 0, file1.ContentLength);
                        }
                        doc = _doc;

                        session.SaveOrUpdate(doc);

                        transaction.Commit();
                        return RedirectToAction("Index");
                    } else
                    {
                        ViewBag.Message = "Имя Документа в базе данных уже существует.";
                    }
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var repository = new DocumentRepositiry();
            var res=repository.GetDocumentBy(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var repository = new DocumentRepositiry();
                    var res = repository.GetDocumentBy(id);
                    if (res == null)
                    {
                        return HttpNotFound();
                    }
                    var criteria = session.CreateCriteria<Document>();
                    criteria.CreateAlias("MyUser", "Person", JoinType.LeftOuterJoin);
                    criteria.SetResultTransformer(new DistinctRootEntityResultTransformer());

                    List<Document> list = new List<Document>();
                    foreach (var i in criteria.List<Document>())
                    {
                        if (i.Id == id)
                        {
                            session.Delete(i);
                            transaction.Commit();
                        }

                    }
                }
            }
            return RedirectToAction("Index");
          
        }   
        
        public ActionResult _Error()
        {
            ViewBag.Message="Для создания Документа требуется: Название, и сам файл. Проверте, все ли Вы сделали верно";
            return PartialView();
        }
       

    }
}
