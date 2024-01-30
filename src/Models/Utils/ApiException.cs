using System.Net;

namespace Project.AuthSystem.API.src.Models.Utils;
public class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public ApiException() : base() { }
    public ApiException(string message) : base(message) { }
    public ApiException(string message, HttpStatusCode statusCode) : base(String.Format(message, statusCode))
    {
        StatusCode = statusCode;
    }
    public ApiException(string message, params object[] args) : base(String.Format(message, args)) { }

}