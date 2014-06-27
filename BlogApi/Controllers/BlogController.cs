using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using BlogApi.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace BlogApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BlogController : ApiController
    {
        [HttpGet]
        public List<BlogApi.Models.Blog> GetAllBlogEntries()
        {
            var retval = new List<Blog>();
            var connectionString = ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (var cmd = new SqlCommand("SELECT EntryID, UserID, EntryText, EntryDate FROM BLOG_ENTRIES", cn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var blogEntry = new Blog
                        {
                            BlogId = reader.GetInt32(0),
                            BlogUser = reader.GetInt32(1),
                            BlogText = reader.GetString(2),
                            BlogDate = reader.GetDateTime(3)
                        };
                        retval.Add(blogEntry);
                    }
                }
            }
            return retval;
        }
    }
}
