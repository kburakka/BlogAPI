using BlogPostAPI.Models;
using BlogPostAPI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Services;

namespace BlogPostAPI.Controllers
{
    public class PostController : ApiController
    {
        DatabaseContext context = new DatabaseContext();

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await context.Posts.ToListAsync());
        }

        public async Task<IHttpActionResult> GetPostbyID(int id)
        {
            Post post = await context.Posts.FindAsync(id);
            if (post == null)
            {
                return Ok("error");
            }
            return Ok(post);
        }

        //Example api/user/username?id=1
        [Route("api/posts/user")]
        public async Task<IHttpActionResult> GetPostsbyUserID(int id)
        {
            List<Post> posts = await context.Posts.Where(x => x.Id == id).ToListAsync();
            var lists = posts.Select(i => new { i.Id, i.PostText, i.PostTitle, i.CreatedDate })
                .Distinct()
                .OrderByDescending(i => i.CreatedDate)
                .ToArray();
            if (posts == null)
            {
                return Ok("error");
            }
            return Ok(lists);
        }

        //Example api/user/username?id=1
        [Route("api/posts/category")]
        public async Task<IHttpActionResult> GetPostsbyCategoryID(int id)
        {
            List<Post> posts = await context.Posts.Where(x => x.Id == id).ToListAsync();
            var lists = posts.Select(i => new { i.Id, i.PostText, i.PostTitle, i.CreatedDate })
                .Distinct()
                .OrderByDescending(i => i.CreatedDate)
                .ToArray();
            if (posts == null)
            {
                return Ok("error");
            }
            return Ok(lists);
        }

        public async Task<IHttpActionResult> PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Posts.Add(post);
            await context.SaveChangesAsync();
            return Ok(post);
        }

        public async Task<IHttpActionResult> PutPost(int id, Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != post.Id)
                return BadRequest();
            context.Entry(post).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return NotFound();
            }
            return Ok("Success");
        }

        public async Task<IHttpActionResult> DeletePost(int id)
        {
            Post post = await context.Posts.FindAsync(id);
            if (post == null)
            {
                return Ok("error");
            }
            try
            {
                if (post.IsDeleted == true)
                {
                    return Ok("Already deleted");
                }
                else
                {
                    post.IsDeleted = true;
                }
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return NotFound();
            }
            return Ok("Success");
        }
    }
}
