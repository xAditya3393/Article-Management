using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6_Project.Areas.Admin.ViewModels;
using WebApplication6_Project.Infrastructure;
using WebApplication6_Project.Models;

namespace WebApplication6_Project.Areas.Admin.Controllers
{

    [Authorize(Roles ="admin")]
    [SelectedTag("users")]

    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View(new UsersIndex
            {

                Users = Database.Session.Query<User>().ToList()
            });
        }


        public ActionResult New()
        {
            return View(new UserNew
            {
                Roles = Database.Session.Query<Role>().Select(role => new RoleCheckBox
                {
                    Id = role.Id,
                    IsChecked = false,
                    Name = role.Name

                }).ToList()

            });
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(UserNew form)
        {

            var user = new User();
            SyncRoles(form.Roles, user.Roles);

            if (Database.Session.Query<User>().Any(u => u.Username == form.UserName))
                ModelState.AddModelError("Username","Username must be unique");

            if (!ModelState.IsValid)
                return View(form);


            user.Email = form.Email;
            user.Username = form.UserName;
            user.SetPassword(form.Password);

            Database.Session.Save(user);

            return RedirectToAction("index");
        }


        public ActionResult Edit(int id) {

            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            return View(new UserEdit
            {
                UserName = user.Username,
                Email = user.Email,
                Roles = Database.Session.Query<Role>().Select(role => new RoleCheckBox
                {
                    Id = role.Id,
                    IsChecked = user.Roles.Contains(role),
                    Name = role.Name

                }).ToList()

            });

        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserEdit form)
        {

            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            SyncRoles(form.Roles, user.Roles);

            if (Database.Session.Query<User>().Any(u => u.Username == form.UserName && u.Id != id))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            user.Username = form.UserName;
            user.Email = form.Email;
            Database.Session.Update(user);

            return RedirectToAction("index");

        }


        public ActionResult ResetPassword(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            return View(new UserResetPassword
            {
                Username = user.Username

            });
        }



        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id, UserResetPassword form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            form.Username = user.Username;

            if (!ModelState.IsValid)
                return View(form);

            user.SetPassword(form.Password);
            Database.Session.Update(user);

            return RedirectToAction("index");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            Database.Session.Delete(user);

            return RedirectToAction("index");
        }


        private void SyncRoles(IList<RoleCheckBox> checkboxes, IList<Role> roles)
        {

            var selectedRoles = new List<Role>();

            foreach (var role in Database.Session.Query<Role>())
            {
                var checkbox = checkboxes.Single(c => c.Id == role.Id);
                checkbox.Name = role.Name;

                if (checkbox.IsChecked)
                    selectedRoles.Add(role);
            }

            foreach (var toAdd in selectedRoles.Where(t => !roles.Contains(t)))
                roles.Add(toAdd);

            foreach (var toRemove in roles.Where(t => !selectedRoles.Contains(t)).ToList())
                roles.Remove(toRemove);
        }

    }
}