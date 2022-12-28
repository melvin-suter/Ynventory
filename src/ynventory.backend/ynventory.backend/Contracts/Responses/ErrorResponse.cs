using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Contracts.Responses
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public Dictionary<string, object?>? Data { get; set; }

        private ErrorResponse()
        {
        }

        public ErrorResponse(int errorCode, int statusCode, string message, Dictionary<string, object?>? data)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public ErrorResponse(YnventoryException exception) : this(exception, ErrorCodes.StatusCode(exception.Code))
        {
        }

        public ErrorResponse(YnventoryException exception, int statusCode) 
            : this(exception.Code, 
                  statusCode, 
                  exception.Message,
                  exception.Data is not null ? new Dictionary<string, object?>(exception.Data) : null)
        {
        }

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

        public ObjectResult ToResult()
        {
            return new ObjectResult(this)
            {
                StatusCode = StatusCode,
            };
        }

        public async Task WriteTo(HttpContext context)
        {
            context.Response.StatusCode = StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(this);
        }
    }
}
