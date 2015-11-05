using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBService
{
    /// <summary>
    /// 服务工厂类
    /// </summary>
    public static class ServiceFactory
    {







        private static Dictionary<Type, BaseService> serviceDictionary = new Dictionary<Type, BaseService>();


        /// <summary>
        /// 获取Service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
            where T : BaseService
        {
            Type typeParameterType = typeof(T);
            if (!serviceDictionary.ContainsKey(typeParameterType))
            {

                serviceDictionary[typeParameterType] = (T)Activator.CreateInstance(typeParameterType, null);
            }

            return (T)serviceDictionary[typeParameterType];
        }





    }
}
