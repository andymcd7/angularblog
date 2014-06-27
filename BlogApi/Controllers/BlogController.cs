using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using BlogApi.Models;

namespace BlogApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BlogController : ApiController
    {
        [HttpGet]
        public List<BlogApi.Models.Blog> GetAllBlogEntries()
        {
            var blogs = new List<Blog>();
            var dummy = new Blog
            {
                BlogDate = DateTime.Now,
                BlogId = 7,
                BlogText = "hello there",
                BlogUser = 2
            };
            blogs.Add(dummy);
            return blogs;
        }

        //[HttpGet]
        //public List<BlogApi.Models.Blog> GetBlogsByUser(int id)
        //{
        //    var blogs = new List<Blog>();
        //    var dummy = new Blog
        //    {
        //        BlogDate = DateTime.Now,
        //        BlogId = 7,
        //        BlogText = "hello there",
        //        BlogUser = id
        //    };
        //    blogs.Add(dummy);
        //    return blogs;
        //}
    }
}
