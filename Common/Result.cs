namespace API.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error? Error { get; }

        protected Result(bool isSuccess, Error? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Ok() => new(true, null);
        public static Result Fail(ErrorType type, string message) => new(false, new Error(type, message));
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(T value) : base(true, null) => Value = value;
        private Result(Error error) : base(false, error) { }

        public static Result<T> Ok(T value) => new(value);
        public static new Result<T> Fail(ErrorType type, string message) => new(new Error(type, message));
    }
}
