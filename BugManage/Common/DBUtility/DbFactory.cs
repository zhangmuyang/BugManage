using System;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.Odbc;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using Zelo.Common.Common;

namespace Zelo.Common.DBUtility
{
    public class DbFactory
    {
        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// ����ȡ��������еĲ�������oracleΪ":",sqlserverΪ"@"
        /// </summary>
        /// <returns></returns>
        public static string CreateDbParmCharacter()
        {
            string character = string.Empty;

            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    character = "@";
                    break;
                case DatabaseType.ORACLE:
                    character = ":";
                    break;
                case DatabaseType.MYSQL:
                    character = "?";
                    break;
                case DatabaseType.ACCESS:
                    character = "@";
                    break;
                case DatabaseType.SQLITE:
                    character = "@";
                    break;
                default:
                    throw new Exception("���ݿ�����Ŀǰ��֧�֣�");
            }

            return character;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ����ͺʹ����
        /// ���ݿ������ַ�����������Ӧ���ݿ����Ӷ���
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDbConnection CreateDbConnection(string connectionString)
        {
            IDbConnection conn = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    conn = new SqlConnection(connectionString);
                    break;
                case DatabaseType.ORACLE:
                    conn = new OracleConnection(connectionString);
                    break;
                case DatabaseType.MYSQL:
                    conn = new MySqlConnection(connectionString);
                    break;
                case DatabaseType.ACCESS:
                    conn = new OleDbConnection(connectionString);
                    break;
                case DatabaseType.SQLITE:
                    conn = new SQLiteConnection(connectionString);
                    break;
                default:
                    throw new Exception("���ݿ�����Ŀǰ��֧�֣�");
            }

            return conn;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// ��������Ӧ���ݿ��������
        /// </summary>
        /// <returns></returns>
        public static IDbCommand CreateDbCommand()
        {
            IDbCommand cmd = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    cmd = new SqlCommand();
                    break;
                case DatabaseType.ORACLE:
                    cmd = new OracleCommand();
                    break;
                case DatabaseType.MYSQL:
                    cmd = new MySqlCommand();
                    break;
                case DatabaseType.ACCESS:
                    cmd = new OleDbCommand();
                    break;
                case DatabaseType.SQLITE:
                    cmd = new SQLiteCommand();
                    break;
                default:
                    throw new Exception("���ݿ�����Ŀǰ��֧�֣�");
            }

            return cmd;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// ��������Ӧ���ݿ�����������
        /// </summary>
        /// <returns></returns>
        public static IDbDataAdapter CreateDataAdapter()
        {
            IDbDataAdapter adapter = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    adapter = new SqlDataAdapter();
                    break;
                case DatabaseType.ORACLE:
                    adapter = new OracleDataAdapter();
                    break;
                case DatabaseType.MYSQL:
                    adapter = new MySqlDataAdapter();
                    break;
                case DatabaseType.ACCESS:
                    adapter = new OleDbDataAdapter();
                    break;
                case DatabaseType.SQLITE:
                    adapter = new SQLiteDataAdapter();
                    break;
                default:
                    throw new Exception("���ݿ�����Ŀǰ��֧�֣�");
            }

            return adapter;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// �ʹ�������������������Ӧ���ݿ�����������
        /// </summary>
        /// <returns></returns>
        public static IDbDataAdapter CreateDataAdapter(IDbCommand cmd)
        {
            IDbDataAdapter adapter = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    adapter = new SqlDataAdapter((SqlCommand)cmd);
                    break;
                case DatabaseType.ORACLE:
                    adapter = new OracleDataAdapter((OracleCommand)cmd);
                    break;
                case DatabaseType.MYSQL:
                    adapter = new MySqlDataAdapter((MySqlCommand)cmd);
                    break;
                case DatabaseType.ACCESS:
                    adapter = new OleDbDataAdapter((OleDbCommand)cmd);
                    break;
                case DatabaseType.SQLITE:
                    adapter = new SQLiteDataAdapter((SQLiteCommand)cmd);
                    break;
                default: throw new Exception("���ݿ�����Ŀǰ��֧�֣�");
            }

            return adapter;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// ��������Ӧ���ݿ�Ĳ�������
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter()
        {
            IDbDataParameter param = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    param = new SqlParameter();
                    break;
                case DatabaseType.ORACLE:
                    param = new OracleParameter();
                    break;
                case DatabaseType.MYSQL:
                    param = new MySqlParameter();
                    break;
                case DatabaseType.ACCESS:
                    param = new OleDbParameter();
                    break;
                case DatabaseType.SQLITE:
                    param = new SQLiteParameter();
                    break;
                default:
                    throw new Exception("���ݿ�����Ŀǰ��֧�֣�");
            }

            return param;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// ��������Ӧ���ݿ�Ĳ�������
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter(string paramName, object value)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// ��������Ӧ���ݿ�Ĳ�������
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter(string paramName, object value, DbType dbType)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.DbType = dbType;
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// ��������Ӧ���ݿ�Ĳ�������
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter(string paramName, object value, ParameterDirection direction)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.Direction = direction;
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// ��������Ӧ���ݿ�Ĳ�������
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter(string paramName, object value, int size, ParameterDirection direction)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.Direction = direction;
            param.ParameterName = paramName;
            param.Value = value;
            param.Size = size;

            return param;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// ��������Ӧ���ݿ�Ĳ�������
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbOutParameter(string paramName, int size)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.Direction = ParameterDirection.Output;
            param.ParameterName = paramName;
            param.Size = size;

            return param;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// ��������Ӧ���ݿ�Ĳ�������
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter(string paramName, object value, DbType dbType, ParameterDirection direction)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.Direction = direction;
            param.DbType = dbType;
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// �ʹ���Ĳ�����������Ӧ���ݿ�Ĳ����������
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter[] CreateDbParameters(int size)
        {
            int i = 0;
            IDbDataParameter[] param = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    param = new SqlParameter[size];
                    while (i < size) { param[i] = new SqlParameter(); i++; }
                    break;
                case DatabaseType.ORACLE:
                    param = new OracleParameter[size];
                    while (i < size) { param[i] = new OracleParameter(); i++; }
                    break;
                case DatabaseType.MYSQL:
                    param = new MySqlParameter[size];
                    while (i < size) { param[i] = new MySqlParameter(); i++; }
                    break;
                case DatabaseType.ACCESS:
                    param = new OleDbParameter[size];
                    while (i < size) { param[i] = new OleDbParameter(); i++; }
                    break;
                case DatabaseType.SQLITE:
                    param = new SQLiteParameter[size];
                    while (i < size) { param[i] = new SQLiteParameter(); i++; }
                    break;
                default:
                    throw new Exception("���ݿ�����Ŀǰ��֧�֣�");

            }

            return param;
        }

        /// <summary>
        /// ���������ļ��������õ����ݿ�����
        /// ��������Ӧ���ݿ���������
        /// </summary>
        /// <returns></returns>
        public static IDbTransaction CreateDbTransaction()
        {
            IDbConnection conn = CreateDbConnection(AdoHelper.ConnectionString);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn.BeginTransaction();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static IDbTransaction CreateDbTransaction(System.Data.IsolationLevel level)
        {
            IDbConnection conn = CreateDbConnection(AdoHelper.ConnectionString);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn.BeginTransaction(level);
        } 
    }
}