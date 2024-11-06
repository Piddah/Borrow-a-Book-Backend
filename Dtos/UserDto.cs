

namespace BorrowABook.Backend.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }

}
