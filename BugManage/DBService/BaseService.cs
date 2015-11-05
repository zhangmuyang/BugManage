using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Zelo.Common.DBUtility;

namespace DBService
{
    public abstract class BaseService
    {

        /// <summary>
        /// 获取数据库连接帮助类
        /// </summary>
        public DBHelper DataBaseHelper
        {
            get
            {
                return DBHelper.GetInstance();
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update<T>(T item)
        {
            item = (T)SetUpdateInfo(item);
            return DataBaseHelper.Update<T>(item);
        }
        public int Update<T>(List<T> entityList)
        {
            List<T> nentityList = new List<T>();
            foreach (T item in entityList)
            {
                nentityList.Add((T)SetUpdateInfo(item));
            }
            return DataBaseHelper.Update<T>(nentityList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        protected Object SetUpdateInfo(Object item)
        {

            SetProPropertyValue("F_EditDATE", item, DateTime.Now);
            return item;
        }

        public int Save<T>(T item)
        {
            item = (T)SetSaveInfo(item);
            return DataBaseHelper.Save<T>(item);
        }

        public int Save<T>(List<T> entityList)
        {
            List<T> nentityList = new List<T>();
            foreach (T item in entityList)
            {
                nentityList.Add((T)SetSaveInfo(item));
            }
            return DataBaseHelper.Save<T>(nentityList);
        }

        protected Object SetSaveInfo(Object item)
        {


            SetProPropertyValue("F_INDATE", item, DateTime.Now);

            SetProPropertyValue("F_EditDATE", item, DateTime.Now);

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        private static void SetProPropertyValue(string propertyName, object instance, Object value)
        {
            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo _findedPropertyInfo = instance.GetType().GetProperty(propertyName);
                if (_findedPropertyInfo != null)
                {
                    if (_findedPropertyInfo.PropertyType.IsGenericType)
                    { _findedPropertyInfo.SetValue(instance, Convert.ChangeType(value, _findedPropertyInfo.PropertyType.GetGenericArguments()[0]), null); }
                    else
                    { _findedPropertyInfo.SetValue(instance, Convert.ChangeType(value, _findedPropertyInfo.PropertyType), null); }
                }
            }

        }







    }
}
