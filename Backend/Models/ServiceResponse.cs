namespace pizza_server.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = null;
        public bool Success { get; set; } = true;
    }
}
