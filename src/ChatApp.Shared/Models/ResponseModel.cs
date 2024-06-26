using ChatApp.Common.Enums;
using System.Net;

namespace ChatApp.Common.Models
{
    public class ResponseModel
    {
        public HttpStatusCode HttpStatus { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public object? Data { get; set; }

        public static ResponseModel Success<T>(T data, HttpStatusCode httpStatus = HttpStatusCode.OK) where T : class
        {
            return new ResponseModel
            {
                HttpStatus = httpStatus,
                IsSuccess = true,
                Data = data,
            };
        }

        public static ResponseModel Error(ErrorResponseModel model)
        {
            return new ResponseModel()
            {
                HttpStatus = GetHttpStatusCode(model.Type),
                IsSuccess = false,
                Data = model
            };

            static HttpStatusCode GetHttpStatusCode(ErrorType type) =>
                type switch
                {
                    ErrorType.Failure => HttpStatusCode.BadRequest,
                    ErrorType.NotFound => HttpStatusCode.NotFound,
                    ErrorType.Validation => HttpStatusCode.BadRequest,
                    ErrorType.Conflict => HttpStatusCode.Conflict,
                    _ => HttpStatusCode.InternalServerError,
                };

        }
    }
}
