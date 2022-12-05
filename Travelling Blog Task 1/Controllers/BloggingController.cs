using BlogTask.Models;
using System.Linq;
using System.Web.Mvc;

namespace BlogTask.Controllers
{
    [Authorize]
    public class BloggingController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProfileData()
        {
            var data = new BlogsTable();
            return View(data);
        }
        public ActionResult AddBlogs()
        {
            if (Session["UserData"] == null)
            {
                TempData["ErrorInfo"] = "Please login before adding blogs";
                TempData.Keep();
                return View();
            }
            var user = Session["UserData"] as UserTable;
            var blog = new BlogsTable();
            blog.UId = user.UserId;
            blog.UserTable = user; ;
            return View(blog);
        }
        [HttpPost]
        public ActionResult AddBlogs(BlogsTable blogsTable)
        {
            var context = new BlogEntities();
            context.BlogsTables.Add(blogsTable);
            context.SaveChanges();
            return View("Index");
        }
        public ActionResult DisplayBlogs()
        {
            var context = new BlogEntities();
            var data = context.BlogsTables.ToList();
            return View(data);
        }

        [AllowAnonymous]
        public ActionResult DisplayAllBlogs()
        {
            var context = new BlogEntities();
            var data = context.BlogsTables.ToList();
            return View(data);
        }

        public ActionResult EditBlogs(string Id)
        {
            var context = new BlogEntities();
            var BId = int.Parse(Id);
            var selected = context.BlogsTables.SingleOrDefault(b => b.BlogId == BId);
            return View(selected);
        }
        [HttpPost]
        public ActionResult UpdateBlogs(BlogsTable blogsTable)
        {
            var context = new BlogEntities();
            var selected = context.BlogsTables.SingleOrDefault(b => b.BlogId == blogsTable.BlogId);
            if (selected == null)
            {
                throw new System.Exception("Blog is not found");
            }
            selected.BlogTitle = blogsTable.BlogTitle;
            selected.UId = blogsTable.UId;
            selected.images = blogsTable.images;
            selected.messages = blogsTable.messages;
            selected.ratings = blogsTable.ratings;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteBlogs(string Id)
        {
            var context = new BlogEntities();
            var BId = int.Parse(Id);
            var model = context.BlogsTables.SingleOrDefault(b => b.BlogId == BId);
            context.BlogsTables.Remove(model);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}