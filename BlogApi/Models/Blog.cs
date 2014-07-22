using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApi.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string BlogText { get; set; }
        public DateTime? BlogDate { get; set; }
        public string BlogUser { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}