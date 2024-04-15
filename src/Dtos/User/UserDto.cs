namespace UserJwt.Dtos.User
{

  public class UserDto
  {
    public string? Id { get; set; } = null;

    public string? Name { get; set; } = null;
    public string? Email { get; set; } = null;
    public string Role { get; set; } = "user";
    public DateTime CreatedAt { get; set; }
  }
}