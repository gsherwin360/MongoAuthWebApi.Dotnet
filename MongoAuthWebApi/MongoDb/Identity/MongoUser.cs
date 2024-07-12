using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace MongoAuthWebApi.MongoDb.Identity;

[CollectionName("users")]
public class MongoUser : MongoIdentityUser<Guid>
{
    public override string? UserName { get => Email; set => Email = value; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    [BsonIgnore]
    public bool IsLockedOut
    {
        get
        {
            if (LockoutEnd.HasValue)
            {
                return LockoutEnd.Value.UtcDateTime > DateTime.UtcNow;
            }

            return false;
        }

        set
        {
            if (LockoutEnd.HasValue && LockoutEnd.Value.UtcDateTime > DateTime.UtcNow)
            {
                LockoutEnd = DateTime.UtcNow;
            }

            AccessFailedCount = 0;
        }
    }

    public DateTime ModifiedOn { get; set; }

    public DateTime LastActivityOn { get; set; }
}
