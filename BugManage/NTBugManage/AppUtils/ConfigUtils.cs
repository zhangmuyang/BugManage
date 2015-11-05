using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NTBugManage.AppUtils
{
    public class ConfigUtils
    {
        public static Boolean IsWebApiDebug()
        {
            String str = ConfigurationManager.AppSettings["IsWebApiDebug"];
            if (str == String.Empty || str == null)
            {
                return false;
            }
            if ("true".ToUpper().Equals(str.ToUpper()))
            {
                return true;
            }
            return false;
        }
    }
}