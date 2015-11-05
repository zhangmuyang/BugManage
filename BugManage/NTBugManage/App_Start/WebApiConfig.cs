using NTBugManage.AppFliter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;


namespace NTBugManage
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Filters.Add(new APIExceptionFilterAttribute());


        }
     



    }
}
