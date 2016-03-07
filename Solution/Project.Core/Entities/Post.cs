using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Entities
{
    public class Post
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }//Kujna
        public DateTime DateCreated { get; set; }
        public string URL { get; set; }
        public string Sostojki { get; set; }
        public string Podgotovka { get; set; }
    }
}
