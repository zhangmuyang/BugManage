using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBugManage.Models
{
    public enum ResultCode
    {
        ServerException = -1,
        Fail = 0,
        Success,
        TokenOutDate
    }
    public class Result<T>
    {
        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Result<T> CreateInstance(ResultCode code)
        {
            return CreateInstance(code, String.Empty);

        }
        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result<T> CreateInstance(ResultCode code, String message)
        {
            Result<T> result = new Result<T>();
            result.code = (int)code;
            result.message = message;

            return result;
        }


        public int code
        {
            get;
            set;
        }
        public String message
        {
            get;
            set;
        }
        public T result_data
        {
            get;
            set;
        }
        /// <summary>
        /// 成功
        /// </summary>
        public void SetSuccess()
        {
            SetSuccess("");

        }
        public void SetSuccess(String message)
        {
            this.code = (int)ResultCode.Success;
            this.message = message;
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message"></param>
        public void SetFail(String message)
        {

            SetError((int)ResultCode.Fail, message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void SetError(int code, String message)
        {
            this.code = code;
            this.message = message;
        }

    }
}