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
using System.Data;

namespace BlogApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BlogController : ApiController
    {

        // todo: test this api, and also check in the service javascript file

        [HttpPost]
        public int AddBlogEntry(Blog blog)
        {
            var connectionString = ConfigurationManager.AppSettings["DatabaseConnection"];
            var retval = -1;

            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (var cmd = new SqlCommand("INSERT INTO BLOG_ENTRIES (UserID, EntryText, EntryDate) VALUES(@USER_ID, @ENTRY_TEXT, @ENTRY_DATE) SELECT @@Identity As BlogID", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@USER_ID", SqlDbType.Int).Value = blog.BlogUser);
                    cmd.Parameters.Add(new SqlParameter("@ENTRY_TEXT", SqlDbType.Text).Value = blog.BlogText);
                    cmd.Parameters.Add(new SqlParameter("@ENTRY_DATE", SqlDbType.DateTime).Value = blog.BlogDate);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retval = reader.GetInt32(0);
                        }
                    }
                }
            }
            return retval;
        }

        [HttpGet]
        [ActionName("GetAllBlogEntries")]
        public List<BlogApi.Models.Blog> GetAllBlogEntries()
        {
            System.Threading.Thread.Sleep(1000);
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

        [HttpGet]
        [ActionName("GetBlogEntry")]
        public List<BlogApi.Models.Blog> GetBlogEntry(int id)
        {
            var retval = new List<Blog>();
            var connectionString = ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (var cmd = new SqlCommand("SELECT EntryID, UserID, EntryText, EntryDate FROM BLOG_ENTRIES WHERE EntryID = @EntryID", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EntryID", SqlDbType.Int).Value = id);
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

            }
            return retval;
        }

        [HttpDelete]
        public bool DeleteBlogEntry(Blog blog)
        {
            var connectionString = ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (var cmd = new SqlCommand("DELETE FROM BLOG_ENTRIES WHERE EntryID = @EntryID", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EntryID", SqlDbType.Int).Value = blog.BlogId);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        [HttpPut]
        public bool UpdateBlogEntry(Blog blog)
        {
            var connectionString = ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (var cmd = new SqlCommand("UPDATE BLOG_ENTRIES SET EntryText = @EntryText, EntryDate = @EntryDate WHERE EntryID = @EntryID", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EntryText", SqlDbType.VarChar).Value = blog.BlogText);
                    cmd.Parameters.Add(new SqlParameter("@EntryDate", SqlDbType.DateTime).Value = blog.BlogDate);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
