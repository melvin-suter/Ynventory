using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ynventory.Backend.Contracts.Responses
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public Dictionary<string, object?>? Data { get; set; }

        public static ErrorResponse NotFound(int code, string message, Dictionary<string, object?>? data = null)
        {
            return new ErrorResponse
            {
                ErrorCode = code,
                Message = message,
                StatusCode = (int)HttpStatusCode.NotFound,
                Data = data,
            };
        }

        public static ErrorResponse Unauthorized(int code, string message, Dictionary<string, object?>? data = null)
        {
            return new ErrorResponse
            {
                ErrorCode = code,
                Message = message,
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Data = data,
            };
        }

        public static ErrorResponse BadRequest(int code, string message, Dictionary<string, object?>? data = null)
        {
            return new ErrorResponse
            {
                ErrorCode = code,
                Message = message,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Data = data,
            };
        }

        public ObjectResult AsResult()
        {
            return new ObjectResult(this)
            {
                StatusCode = StatusCode,
            };
        }
    }
}
