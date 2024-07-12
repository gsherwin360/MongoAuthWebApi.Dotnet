using MongoAuthWebApi.MongoDb.Identity;

namespace MongoAuthWebApi.Models.DTOs;

public class UserDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime? LastActivityOn { get; set; }

    public static IEnumerable<UserDTO> ToMongoUserDTOMapList(IEnumerable<MongoUser> source)
    {
        if (source is null)
        {
            throw new InvalidOperationException("Cannot mapped into MongoUser");
        }

        return source.Select(item => new UserDTO
        {
            Id = item.Id,
            FirstName = item.FirstName,
            LastName = item.LastName,
            Email = item.Email!,
            CreatedOn = item.CreatedOn,
            LastActivityOn = item.LastActivityOn
        });
    }
}