using Project.Core;
using Project.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Web;




namespace Project.Infrastructure.Repository
{
    public class UserRepository
    {
        private DatabaseContext db = new DatabaseContext();

        
        public List<User> GetAll()
        {
            List<User> users = db.Users.ToList();
            return users;
        }
        public void Create(User newUser)
        {
            db.Users.Add(newUser);
            db.SaveChanges();

        }

      


        public User GetByUsername(string username)
        {
            return db.Users.FirstOrDefault(x=>x.Username==username);
        }
        public User GetById(int id)
        {
            return db.Users.FirstOrDefault(x=>x.ID==id);
        }

        //public User GetCurrent()
        //{
        //    User user = System.Web.HttpContext.Current.User.Identity.Name;
        //    return GetByUsername(user);
        //}

    }
}
