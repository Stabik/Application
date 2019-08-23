using ClassLibrary.model;
using ClassLibrary.model.Repository;
using MvcStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStudy.Controllers
{
    public class PersonController : Controller
    {
        private PersonRepository personRepository;
        private GroupRepository groupRepository;

        public PersonController(PersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public ActionResult Create()
        {
            var model = new PersonModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PersonModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Group group = new Group()
            {
                NameGroup=model.group.ToString()
            };
            var person = new Person
            {
                Login = model.Login,
                Password = model.Password,
                Group = model.group
            };
            groupRepository.Save(group);
            personRepository.Save(person);

            return RedirectToAction("Index", "Home");
        }
    }
}