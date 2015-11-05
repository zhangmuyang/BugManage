using DBService;
using NTBugManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NTBugManage.Controllers
{
    public abstract class BaseController : ApiController
    {

        public BaseController()
        {



        }

     



     

        public T GetService<T>() where T : BaseService
        {
            return ServiceFactory.GetService<T>();
        }

   
    }
}
