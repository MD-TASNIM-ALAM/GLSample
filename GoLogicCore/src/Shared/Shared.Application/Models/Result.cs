using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Application.Models
{
    public class Result<T>
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public T Payload { get; set; }

        public List<string> Errors { get; set; }

        protected Result() { }

        public Result(T payload)
        {
            Message = "OK";
            Payload = payload;
            Success = true;
        }

        public static Result<T> Error(int code, string message = "", List<string> errors = null)
        {
            return new Result<T>
            {
                Success = false,
                StatusCode = code,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
