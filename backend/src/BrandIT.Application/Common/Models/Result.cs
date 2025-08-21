namespace BrandIT.Application.Common.Models;

public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();

    public static Result SuccessResult(string message = "")
        => new() { Success = true, Message = message };

    public static Result FailureResult(string message, params string[] errors)
        => new() { Success = false, Message = message, Errors = errors.ToList() };

    public static Result FailureResult(string message, List<string> errors)
        => new() { Success = false, Message = message, Errors = errors };
}

public class Result<T> : Result
{
    public T? Data { get; set; }

    public static Result<T> SuccessResult(T data, string message = "")
        => new() { Success = true, Data = data, Message = message };

    public static new Result<T> FailureResult(string message, params string[] errors)
        => new() { Success = false, Message = message, Errors = errors.ToList() };

    public static new Result<T> FailureResult(string message, List<string> errors)
        => new() { Success = false, Message = message, Errors = errors };
}
