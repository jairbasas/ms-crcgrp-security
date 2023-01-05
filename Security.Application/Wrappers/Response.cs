
namespace Security.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data, string? message = null)
        {
            this.succeeded = true;
            this.message = message;
            this.data = data;
        }

        public Response(string message)
        {
            this.succeeded = false;
            this.message = message;
        }

        public bool succeeded { get; set; }
        public string message { get; set; }
        public List<string> errors { get; set; }
        public T data { get; set; }

    }
}
