using YetAnotherECommerce.Modules.Users.Core.Entities;

namespace YetAnotherECommerce.Modules.Users.Core.DAL.Mongo.Documents
{
    public static class Extensions
    {
        public static UserDocument AsDocument(this User entity)
            => new UserDocument
            {
                Id = entity.Id.Value,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Address = entity.Address,
                IsRegistrationCompleted = entity.IsRegistrationCompleted,
                CreatedAt = entity.CreatedAt,
                LastUpdatedAt = entity.LastUpdatedAt
            };

        public static User AsEntity(this UserDocument document)
            => new User(document.Id,
                document.FirstName,
                document.LastName,
                document.Email,
                document.Address,
                document.IsRegistrationCompleted,
                document.CreatedAt,
                document.LastUpdatedAt);
    }
}