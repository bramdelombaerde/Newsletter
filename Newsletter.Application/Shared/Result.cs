using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newsletter.Application.Shared
{
    public interface IResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        string Error { get; }
        ErrorCode ErrorCode { get; }
        IResult<T> ToResult<T>(T? value = default);
    }

    public interface IResult<T> : IResult
    {
        T? Value { get; }
    }

    // These ErrorCodes will be used to map Application Results to proper HTTP Responses.
    public enum ErrorCode
    {
        None = 0,
        General = 1,
        NotFound = 2,
        Validation = 4,
        BusinessRule = 8
    }

    internal class Result : IResult
    {
        protected Result(bool isSuccess, string error, ErrorCode errorCode = ErrorCode.None)
        {
            IsSuccess = isSuccess;
            Error = error;
            ErrorCode = errorCode;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; private set; }
        public ErrorCode ErrorCode { get; private set; }

        public static IResult Success() => new Result(true, string.Empty);
        public static IResult<T> Success<T>(T value) => new Result<T>(true, string.Empty, value);
        public static IResult Fail(string message) => new Result(false, message, ErrorCode.General);
        public static IResult<T> Fail<T>(string message) => new Result<T>(false, message, default, ErrorCode.General);
        public static IResult NotFound(string message) => new Result(false, message, ErrorCode.NotFound);
        public static IResult<T> NotFound<T>(string message) => new Result<T>(false, message, default, ErrorCode.NotFound);
        public static IResult<T> ValidationError<T>(string message) => new Result<T>(false, message, default, ErrorCode.Validation);
        public static IResult BusinessRuleError(string message) => new Result(false, message, ErrorCode.BusinessRule);
        public static IResult<T> BusinessRuleError<T>(string message) => new Result<T>(false, message, default, ErrorCode.BusinessRule);

        public static IResult<T> From<T>(IResult result, T? value = default) => new Result<T>(result.IsSuccess, result.Error, value, result.ErrorCode);
        public IResult<T> ToResult<T>(T? value = default) => new Result<T>(IsSuccess, Error, value, ErrorCode);
    }


    internal class Result<T> : Result, IResult<T>
    {
        public Result(bool isSuccess, string message, T? value, ErrorCode errorCode = ErrorCode.None) : base(isSuccess, message, errorCode)
        {
            Value = value;
        }

        public T? Value { get; private set; } = default;
    }
}
