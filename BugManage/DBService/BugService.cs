using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zelo.Common.DBUtility;
using Zelo.Common.Common;


namespace DBService
{
    public class BugService:BaseService
    {

        public Boolean AddNewBug(T_BugList tbList)
        {

            return Save<T_BugList>(tbList) > 0 ? true : false;
        }

        public List<T_BugList> BugList()
        {
            String strSql = "select * from T_BugList order by F_INDATE desc ";
            //ParamMap param = ParamMap.newMap();
            //param.setParameter("myPhone", phoneNumber);
            //param.setParameter("password", passwordEncrypt);
            return DataBaseHelper.FindBySql<T_BugList>(strSql);
            //return DataBaseHelper.FindOne<T_BugList>(strSql, param);
        }
    }
}
