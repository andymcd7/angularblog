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

        /*
         * todo:
         *      
         * blog.html
         *      add delete buttons on blog entries
         *      convert blog results to ng-grid
         *      
         * addBlog.html
         *      add fields
         *      clean up look
         *      
         * updateBlog.html (new)
         * 
         * additional features:
         *      large text area with formatting features (generate html and save in table?)
         *      camera integration?
         *      draw/paint pane?
         * */

        private int GetUserIdByUserName(string userName)
        {
            int retval = -1;
            var connectionString = ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (var cmd = new SqlCommand("SELECT UserID FROM BLOG_USER WHERE UserName = @USER_NAME", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@USER_NAME", SqlDbType.VarChar) { Value = userName });
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

        [HttpPost]
        public Blog AddBlogEntry(Blog blog)
        {
            var connectionString = ConfigurationManager.AppSettings["DatabaseConnection"];

            var userId = GetUserIdByUserName(blog.BlogUser);
            if (blog.BlogDate == null)
            {
                blog.BlogDate = System.DateTime.Now;
            }
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (var cmd = new SqlCommand("INSERT INTO BLOG_ENTRIES (UserID, EntryText, EntryDate) VALUES(@USER_ID, @ENTRY_TEXT, @ENTRY_DATE) SELECT @@Identity As BlogID", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@USER_ID", SqlDbType.Int) {Value = userId});
                    cmd.Parameters.Add(new SqlParameter("@ENTRY_TEXT", SqlDbType.Text) {Value = blog.BlogText});
                    cmd.Parameters.Add(new SqlParameter("@ENTRY_DATE", SqlDbType.DateTime){Value = blog.BlogDate});
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            blog.BlogId = Convert.ToInt32(reader.GetDecimal(0));
                        }
                    }
                }
            }
            return blog;
        }

        [HttpGet]
        [ActionName("GetBlogEntriesByUser")]
        public List<BlogApi.Models.Blog> GetBlogEntriesByUser(string userName)
        {
            var retval = new List<Blog>();
            var connectionString = ConfigurationManager.AppSettings["DatabaseConnection"];

            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (var cmd = new SqlCommand("SELECT E.EntryID, U.UserName, E.EntryText, E.EntryDate FROM BLOG_ENTRIES E JOIN BLOG_USER U ON E.UserID = U.UserID WHERE U.UserName = @USER_NAME", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@USER_NAME", SqlDbType.VarChar) { Value = userName });
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var blogEntry = new Blog
                            {
                                BlogId = reader.GetInt32(0),
                                BlogUser = reader.GetString(1),
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
        public bool DeleteBlogEntry(int blogId)
        {
            var connectionString = ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (var cmd = new SqlCommand("DELETE FROM BLOG_ENTRIES WHERE EntryID = @EntryID", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EntryID", SqlDbType.Int) { Value = blogId });
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
