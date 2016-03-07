using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Core;
using Project.Infrastructure.Data;
using Project.Infrastructure.Repository;
using System.Web.Security;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Project.Controllers
{
    public class UsersController : Controller
    {
       
        private DatabaseContext db = new DatabaseContext();
        public UserRepository userRepository = new UserRepository();

        public UsersController()
        {
           
        }

  



        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Email,Image")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string username, string password, string email)
        {
            User user = new User();
            user.Username = username;
            user.Password = password;
            user.Email = email;
            user.Image = "https://s-media-cache-ak0.pinimg.com/736x/fd/0c/55/fd0c559856ca991e9e28937dc802f0b0.jpg";
            userRepository.Create(user);

            return RedirectToAction("LogIn", new { username = username, password = password });

        }
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(string username, string password)
        {
            var af = new AuthenticationFactory();
            var res = af.LogIn(username,password);
            if (res == AuthStatus.Done)
            {
                var temp = db.Users.FirstOrDefault(x=>x.Username==username);
                string name = username + "("+temp.ID+")";
                FormsAuthentication.SetAuthCookie(name, true);
                //var claim = (User.Identity as ClaimsIdentity);
                //if (claim!=null)
                //{
                //    claim.AddClaim(new Claim("Id", "5"));
                //}
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("LogIn");
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        

    }
}
