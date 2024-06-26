using ChatApp.Common.Enums;

namespace ChatApp.Common.Models;

public class ErrorResponseModel
{
    public string ErrorCode { get; private set; }
    public string ErrorMessage { get; private set; }
    public ErrorType Type { get; set; }

    private ErrorResponseModel(string code, string message, ErrorType errorType)
    {
        ErrorCode = code;
        ErrorMessage = message;
        Type = errorType;
    }

    public static ErrorResponseModel Failure(string code, string message)
        => new(code, message, ErrorType.Failure);

    public static ErrorResponseModel Validation(string code, string message)
       => new(code, message, ErrorType.Validation);

    public static ErrorResponseModel NotFound(string code, string message)
       => new(code, message, ErrorType.NotFound);

    public static ErrorResponseModel Conflict(string code, string message)
       => new(code, message, ErrorType.Conflict);
}
