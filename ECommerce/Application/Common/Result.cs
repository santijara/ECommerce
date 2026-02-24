namespace ECommerce.Application.Common
{
    /// <summary>
    /// Representa el resultado de una operación con valor de retorno.
    /// Result Pattern
    /// Hereda de Result e incluye el valor cuando la operación es exitosa.
    /// </summary>

    public class Result
    {
        public bool IsSuccess { get;}
        public bool IsFailure => !IsSuccess;
        public string Error { get;}

        public Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() =>new(true,string.Empty);
        public static Result Failure(string error) => new(false, error);

      
    }

    public class Result<T> : Result
    {
        public T? Value { get;}

        public Result(bool isSucces,string error, T? data) : base(isSucces, error)
        {
            Value = data;
        }

        public static Result<T>Success(T data)=> new(true,string.Empty,data);
        public static Result<T> Failure(string message) => new(false, message, default);
    }

    
}
