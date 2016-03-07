using Project.Core.Entities;
using Project.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public class PostRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public void Create(Post newPost)
        {
            newPost.DateCreated = DateTime.Now;

            
        }
        public List<Post> GetAll()
        {
            List<Post> posts = db.Posts.ToList();
            return posts;
        }

        public Post GetByTitle(string title)
        {
            return db.Posts.FirstOrDefault(x => x.Title == title);
        }
        public Post GetById(int id)
        {
            return db.Posts.FirstOrDefault(x => x.ID == id);
        }
        public IEnumerable<Post> GetByTag(string tag)
        {
            List<Post> posts = new List<Post>();
            foreach (var post in db.Posts)
            {
                if (post.Tags.Contains(tag))
                {
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
