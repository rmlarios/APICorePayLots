using System.Data;
using System;
using System.Collections.Generic;

namespace Dapper.Application.Wrappers
{
    public class Response<T>
    {
         public Response()
        {
        }
        public Response(List<T> datas)
        {
            Succeeded = true;
            Datas = datas;
        }
        public Response(T data,string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
        public List<T> Datas {get; set;}
    }
}
