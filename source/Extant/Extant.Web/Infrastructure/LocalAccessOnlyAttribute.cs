using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Extant.Web.Infrastructure
{
    public class LocalAccessOnlyAttribute: AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // authentication is not required so no need to call base implementation:
            // only requirement is that the request is made directly from the server
            return httpContext.Request.IsLocal;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        { 
            // if the request is not made from the server we want to deny this controller even exists
            // by returning a 404 error to the client requesting it.
            filterContext.Result = new HttpNotFoundResult();
        }
    }
}