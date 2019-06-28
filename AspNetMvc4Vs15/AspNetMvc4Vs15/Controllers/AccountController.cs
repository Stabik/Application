using AspNetMvc4Vs15.Models;
using NHibernate;
using System.Web.Mvc;
using System.Web.Security;

namespace AspNetMvc4Vs15.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Person user = null;
                using (ISession session = HibernateHelper.OpenSession())
                {
                    user = session.QueryOver<Person>().Where(x => x.Name == model.Name).SingleOrDefault();

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegModel model)
        {
            if (ModelState.IsValid)
            {
                Person person =null;
                using (ISession session = HibernateHelper.OpenSession())
                {
                    person = session.QueryOver<Person>().Where(x => x.Name == model.Name&&x.Password==model.Password).SingleOrDefault();

                }
                if (person == null)
                {
                    
                    using (ISession session = HibernateHelper.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {

                            try
                            {
                                Person user = new Person();
                                user.Name = model.Name;
                                user.Password =model.Password;
                                session.Save(user);
                                transaction.Commit();
                            }
                            catch { }
                            person = session.QueryOver<Person>().Where(x => x.Name == model.Name && x.Password == model.Password).SingleOrDefault();
                        }
                        if (person != null)
                        {
                            FormsAuthentication.SetAuthCookie(model.Name,true);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким Логином уже существует!");
                }
            }
            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }

}

