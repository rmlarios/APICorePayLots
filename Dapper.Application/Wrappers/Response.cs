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
    public Response(List<T> datas,int count=0)
    {
      Succeeded = true;
      Datas = datas;
      Count = count!=0? count : Datas.Count;
    }
    public Response(T data, string message = null)
    {
      Succeeded = true;
      Message = message;
      Data = data;
    }
    public Response(string message, bool success = false)
    {
      Succeeded = success;
      Message = message;
    }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
    public T Data { get; set; }
    public List<T> Datas { get; set; }
    public int Count { get; set; } = 0;
  }
}
