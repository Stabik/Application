using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AspNetMvc4Vs15.Models;
using NHibernate;

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

        public ActionResult UserLogin()
        {
            HttpCookie login = Request.Cookies["Login"];

            if (login != null)
            {
                ViewBag.Message ="Добро пожаловать, "+login.Value+" !";

            }
            return PartialView(ViewBag.Message);
        }
        public string UsLog()
        {
            HttpCookie login = Request.Cookies["Login"];
            return (login.Value);
        }
        public ActionResult Index(string serching, string Author)

        {
            UserLogin();
            var repository = new DocumentRepositiry();
            List<Document> result=new List<Document>();
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
        [HttpPost]
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            var repository = new DocumentRepositiry();
            List<Document> result = new List<Document>();
            if (startDate<DateTime.MinValue&&endDate<DateTime.MinValue)
            {
                result = repository.Doclist();
            }
             result = repository.GetDocumentByDate(startDate, endDate);
             return View(result);
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
                    var _doc = session.Get<Document>(doc.Id);

                    doc.Author = UsLog();
                    doc.Date = DateTime.Now;
                    _doc.Author = doc.Author;
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
            
            var repository = new DocumentRepositiry();
           
            var res = repository.GetDocumentBy(id);

            if (res == null)
            {
                return HttpNotFound();
            }
            
            repository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Document doc, MyFile myFile, HttpPostedFileBase file1)
        {
            if (doc.Name == null || file1 == null)
            {
                return RedirectToAction("_Error");
            }
            var repository = new DocumentRepositiry();
            if (file1 != null)
            {
                doc.MyDoc = new MyFile();

                doc.MyDoc.MyDocName = file1.FileName;
                doc.MyDoc.MyDocFile = new byte[file1.ContentLength];
                file1.InputStream.Read(doc.MyDoc.MyDocFile, 0, file1.ContentLength);
            }
            doc.Author = UsLog();
            repository.Add(doc);
            return RedirectToAction("Index");
        }
        public ActionResult _Error()
        {
            ViewBag.Message="Для создания Документа требуется: Название, и сам файл. Проверте, все ли Вы сделали верно";
            return PartialView();
        }
       

    }
}
