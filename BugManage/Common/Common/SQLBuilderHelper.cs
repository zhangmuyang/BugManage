﻿using System;
using System.Collections.Generic;
using System.Text;
using Zelo.Common.DBUtility;
using System.Data;

namespace Zelo.Common.Common
{
    public class SQLBuilderHelper
    {
        private static string mssqlPageTemplate = "select * from (select ROW_NUMBER() OVER(order by {0}) AS RowNumber, {1}) as tmp_tbl where RowNumber BETWEEN @pageStart and @pageEnd ";
        private static string mysqlOrderPageTemplate = "{0} order by {1} limit ?offset,?limit";
        private static string mysqlPageTemplate = "{0} limit ?offset,?limit";
        private static string sqliteOrderPageTemplate = "{0} order by {1} limit @offset,@limit";
        private static string sqlitePageTemplate = "{0} limit @offset,@limit";
        private static string accessPageTemplate = "select * from (select top @page_limit * from (select top @page_offset {0} order by id desc) order by id) order by {1}";

        public static string fetchColumns(string strSQL)
        {
            string lowerSQL = strSQL.ToLower();
            String columns = lowerSQL.Substring(6, lowerSQL.IndexOf("from") - 6);
            return columns;
        }

        public static string fetchPageBody(string strSQL)
        {
            string body = strSQL.Substring(6, strSQL.Length - 6);
            return body;
        }

        public static string fetchWhere(string strSQL)
        {
            int index = strSQL.LastIndexOf("where");
            if (index == -1) return "";

            String where = strSQL.Substring(index, strSQL.Length - index);
            return where;
        }

        public static bool isPage(string strSQL)
        { 
            string strSql = strSQL.ToLower();

            if (AdoHelper.DbType == DatabaseType.ACCESS && strSql.IndexOf("top") == -1)
            {
                return false;
            }

            if (AdoHelper.DbType == DatabaseType.SQLSERVER && strSql.IndexOf("row_number()") == -1)
            {
                return false;
            }

            if(AdoHelper.DbType == DatabaseType.MYSQL && strSql.IndexOf("limit") == -1)
            {
                return false;
            }

            if (AdoHelper.DbType == DatabaseType.SQLITE && strSql.IndexOf("limit") == -1)
            {
                return false;
            }

            if (AdoHelper.DbType == DatabaseType.ORACLE && strSql.IndexOf("rowid") == -1)
            {
                return false;
            }

            return true;
        }

        public static string builderPageSQL(string strSql, string order, bool desc)
        {
            string columns = fetchColumns(strSql);
            string orderBy = order + (desc ? " desc " : " asc ");
            

            if (AdoHelper.DbType == DatabaseType.SQLSERVER && strSql.IndexOf("row_number()") == -1)
            {
                if (string.IsNullOrEmpty(order))
                {
                    throw new Exception(" SqlException: order field is null, you must support the order field for sqlserver page. ");
                }

                string pageBody = fetchPageBody(strSql);
                strSql = string.Format(mssqlPageTemplate, orderBy, pageBody);
            }

            if (AdoHelper.DbType == DatabaseType.ACCESS && strSql.IndexOf("top") == -1)
            {
                if (string.IsNullOrEmpty(order))
                {
                    throw new Exception(" SqlException: order field is null, you must support the order field for sqlserver page. ");
                }

                //select {0} from (select top @pageSize {1} from (select top @pageSize*@pageIndex {2} from {3} order by {4}) order by id) order by {5}
                string pageBody = fetchPageBody(strSql);
                strSql = string.Format(accessPageTemplate, pageBody, orderBy);
            }

            if (AdoHelper.DbType == DatabaseType.MYSQL)
            {
                if (!string.IsNullOrEmpty(order))
                {
                    strSql = string.Format(mysqlOrderPageTemplate, strSql, orderBy);
                }
                else
                {
                    strSql = string.Format(mysqlPageTemplate, strSql);
                }
            }

            if (AdoHelper.DbType == DatabaseType.SQLITE)
            {
                if (!string.IsNullOrEmpty(order))
                {
                    strSql = string.Format(sqliteOrderPageTemplate, strSql, orderBy);
                }
                else
                {
                    strSql = string.Format(sqlitePageTemplate, strSql);
                }
            }
            
            return strSql;
        }

        public static string builderCountSQL(string strSQL)
        {
            int index = strSQL.IndexOf("from");
            string strFooter = strSQL.Substring(index, strSQL.Length - index);
            string strText = "select count(*) " + strFooter;

            return strText;
        }
        public static string builderAccessPageSQL(string strSql, ParamMap param)
        {
            return builderAccessPageSQL(strSql, param, -1);
        }

        public static string builderAccessPageSQL(string strSql, ParamMap param, int limit)
        {
            if (AdoHelper.DbType != DatabaseType.ACCESS)
            {
                return strSql;
            }

            if (param.ContainsKey("page_limit"))
            {
                if (limit != -1)
                {
                    strSql = strSql.Replace("@" + "page_limit", limit.ToString());
                }
                else
                {
                    strSql = strSql.Replace("@" + "page_limit", param.getString("page_limit"));
                }
                param.Remove("page_limit");
            }

            if (param.ContainsKey("page_offset"))
            {
                strSql = strSql.Replace("@" + "page_offset", param.getString("page_offset"));
                param.Remove("page_offset");
            }

            return strSql;
        }

        public static string builderAccessSQL(Type classType, TableInfo tableInfo, string strSql, IDbDataParameter[] parameters)
        {
            if (AdoHelper.DbType != DatabaseType.ACCESS)
            {
                return strSql;
            }

            foreach (IDbDataParameter param in parameters)
            {
                if (param.Value == null) continue;

                string paramName = param.ParameterName;
                string paramValue = param.Value.ToString();

                float i = 0;
                if (tableInfo.ColumnToProp.ContainsKey(paramName))
                {
                    string propertyName = tableInfo.ColumnToProp[paramName].ToString();
                    Type type = ReflectionHelper.GetPropertyType(classType, propertyName);

                    string typeName = TypeUtils.GetTypeName(type);
                    if (typeName == "System.String" || typeName == "System.DateTime")
                    {
                        paramValue = "'" + paramValue + "'";
                    }
                }
                else if (!float.TryParse(paramValue, out i)) {
                    paramValue = "'" + paramValue + "'";
                }

                //paramName = paramName.ToLower();
                strSql = strSql.Replace("@"+paramName, paramValue);
            }

            return strSql;
        }

        public static string builderAccessSQL(string strSql, IDbDataParameter[] parameters)
        {
            if (AdoHelper.DbType != DatabaseType.ACCESS)
            {
                return strSql;
            }

            foreach (IDbDataParameter param in parameters)
            {
                if (param.Value == null) continue;

                string paramName = param.ParameterName;
                string paramValue = param.Value.ToString();

                paramValue = "'" + paramValue + "'";
                strSql = strSql.Replace("@" + paramName, paramValue);
            }

            return strSql;
        }
    }
}
