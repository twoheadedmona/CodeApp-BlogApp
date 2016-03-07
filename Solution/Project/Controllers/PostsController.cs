using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Core.Entities;
using Project.Infrastructure.Data;
using Project.Infrastructure.Repository;

namespace Project.Controllers
{
   
    public class PostsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        public PostRepository postRepository = new PostRepository();
        // GET: Posts
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // GET: Posts/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        
        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }


        protected int GetLoggedInUserId()
        {
            if (User == null || User.Identity == null)
                return 0;

            string name = User.Identity.Name;
            int firstIndex = name.IndexOf("(");
            int lastIndex = name.LastIndexOf(")");
            string id = name.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
            return int.Parse(id);
        }
        
        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Tags,DateCreated, URL, Tags, Sostojki, Podgotovka")] Post post)
        {
            if (ModelState.IsValid)
            {
                var newPost = post;
                newPost.ID = post.ID;
                newPost.Title = post.Title;
                newPost.Description = post.Description;
                newPost.DateCreated = post.DateCreated;
                newPost.URL = post.URL;
                newPost.Tags = post.Tags;
                newPost.Sostojki = post.Sostojki;
                newPost.Podgotovka = post.Podgotovka;
                var userId = GetLoggedInUserId();
                var thisUser=db.Users.FirstOrDefault(x=>x.ID==userId);
                thisUser.Posts.Add(newPost);
                db.Posts.Add(newPost);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Tags,DateCreated, URL, Tags, Sostojki, Podgotovka")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Search(string query)
        {
            List<Post> res = new List<Post>();
            res.AddRange(postRepository.GetAll().Where(post => post.Title.Contains(query)));
            
            return View("Index", res);
        }

        
    }
}
