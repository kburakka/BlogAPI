using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BlogPostAPI.Models.EntityFramework
{
    public class BaseController : ApiController
    {
        protected static DatabaseContext context;
        protected static object _lockSync = new object();

        protected BaseController()
        {
            CreateContext();
        }

        private static void CreateContext()
        {
            if (context == null)
            {
                lock (_lockSync)
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }
                }
            }
        }
    }
}