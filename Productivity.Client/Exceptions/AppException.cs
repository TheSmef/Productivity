namespace Productivity.Client.Exceptions
{
    public class AppException : Exception
    {
        public string Title { get; set; } = string.Empty;

        public AppException(string title, string message) : base(message) { Title = title; }
    }
}
