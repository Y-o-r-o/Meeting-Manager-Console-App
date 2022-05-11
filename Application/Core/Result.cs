namespace Application.Core
{
   public class Result
    {
        public bool IsSuccess { get; set;} = false;
        public string Error {get; set;} = string.Empty;

        public static Result Success() => new Result {IsSuccess = true};
        public static Result Failure(string error) => new Result {IsSuccess = false, Error = error};
    }
    public class Result<T>
    {
        public bool IsSuccess { get; set;} = false;

        public T Value {get; set;}

        public string Error {get; set;} = string.Empty;

        public static Result<T> Success(T value) => new Result<T> {IsSuccess = true, Value = value};
        public static Result<T> Failure(string error) => new Result<T> {IsSuccess = false, Error = error};
    }

}