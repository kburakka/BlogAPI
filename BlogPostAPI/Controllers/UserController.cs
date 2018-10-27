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
    public class UserController : ApiController
    {
        DatabaseContext context = new DatabaseContext();

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await context.Users.FindAsync());
        }

        public async Task<IHttpActionResult> GetUserbyID(int id)
        {
            User user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return Ok("error");
            }
            return Ok(user);
        }

        //Example api/user/name?id=1
        [Route("api/user/name")]
        public async Task<IHttpActionResult> GetNamebyUserID(int id)
        {
            User user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return Ok("error");
            }
            return Ok(user.Firstname);
        }

        //Example api/user/surname?id=1
        [Route("api/user/surname")]
        public async Task<IHttpActionResult> GetSurnamebyUserID(int id)
        {
            User user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return Ok("error");
            }
            return Ok(user.Lastname);
        }

        //Example api/user/email?id=1
        [Route("api/user/email")]
        public async Task<IHttpActionResult> GetEmailbyUserID(int id)
        {
            User user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return Ok("error");
            }
            return Ok(user.Email);
        }

        //Example api/user/username?id=1
        [Route("api/user/username")]
        public async Task<IHttpActionResult> GetUsernamebyUserID(int id)
        {
            User user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return Ok("error");
            }
            return Ok(user.Username);
        }

        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Ok(user);
        }

        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != user.Id)
                return BadRequest();
            context.Entry(user).State = EntityState.Modified;
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

        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return Ok("error");
            }
            try
            {
                if (user.IsActive == false)
                {
                    return Ok("Already not active");
                }
                else
                {
                    user.IsActive = false;
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
