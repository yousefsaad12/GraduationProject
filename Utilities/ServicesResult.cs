

namespace GraduationProject.Utilities
{
    public class ServicesResult<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public string? Error { get; set; }

        public static ServicesResult<T> Ok(T data, string? message = null)
            => new() { Success = true, Data = data, Message = message };

        public static ServicesResult<T> Fail(string error, string? message = null)
            => new() { Success = false, Error = error, Message = message };
    }
}