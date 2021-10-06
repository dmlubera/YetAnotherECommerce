using YetAnotherECommerce.Modules.Identity.Core.Entities;

namespace YetAnotherECommerce.Modules.Identity.Core.DAL.Mongo.Documents
{
    public static class Extensions
    {
        public static User AsEntity(this UserDocument document)
            => new User(document.Id,
                document.Email,
                document.PasswordHash,
                document.PasswordSalt,
                document.Role,
                document.CreatedAt);

        public static UserDocument AsDocument(this User entity)
            => new UserDocument
            {
                Id = entity.Id,
                Email = entity.Email,
                PasswordHash = entity.Password.Hash,
                PasswordSalt = entity.Password.Salt,
                Role = entity.Role,
                CreatedAt = entity.CreatedAt
            };
    }
}