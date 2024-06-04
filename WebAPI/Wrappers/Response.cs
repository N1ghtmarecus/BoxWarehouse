namespace WebAPI.Wrappers
{
    public class Response<T> : Response
    {
        public T? Data { get; set; }

        public Response()
        {
        }

        public Response(T data)
        {
            Data = data;
            Succeeded = true;
        }
    }

    public class Response
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public Response()
        {
        }

        public Response(bool succeeded, string message, IEnumerable<string>? errors = null)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = errors;
        }
    }
}
