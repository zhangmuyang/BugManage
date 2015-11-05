using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace NTBugManage.AppFliter
{
    public class EnableCorsAttribute : ActionFilterAttribute
    {
        private const string origin = "Origin";
        private const string accessControlAllowOrigin = "Access-Control-Allow-Origin";

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Request.Headers.Contains(origin))
            {
                var originHeader = actionExecutedContext.Request.Headers.GetValues(origin).FirstOrDefault();

                if (!string.IsNullOrEmpty(originHeader))
                {
                    actionExecutedContext.Response.Headers.Add(accessControlAllowOrigin, originHeader);
                }
            }
        }
    }
}