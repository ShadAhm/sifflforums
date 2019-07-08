using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Api.Models
{
    public class RequestResult<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }

        public static RequestResult<T> Success(T data)
        {
            return new RequestResult<T>() { IsSuccess = true, ErrorMessage = null, Data = data };
        }

        public static RequestResult<T> Fail(string message)
        {
            return new RequestResult<T>() {IsSuccess = false, ErrorMessage = message, Data = default(T)};
        }
    }
}
