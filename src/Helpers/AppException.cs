
namespace UserJwt.Helpers
{
  public class AppException : System.Exception
  {
    public readonly int StatusCode;

    public AppException() : base() { }

    public AppException(int code)
    {
      this.StatusCode = code;
    }

    public AppException(string message) : base(message) { }

    public AppException(int code, string message) : base(message)
    {
      this.StatusCode = code;
    }

    public object ToJson()
    {
      return new { StatusCode, Message = this.Message };
    }

  }
}
