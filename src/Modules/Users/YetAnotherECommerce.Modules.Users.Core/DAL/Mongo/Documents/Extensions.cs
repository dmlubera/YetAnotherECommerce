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
                Password = entity.Password, 
                Email = entity.Email,
                Address = entity.Address,
                IsRegistrationCompleted = entity.IsRegistrationCompleted
            };

        public static User AsEntity(this UserDocument document)
            => new User(document.Id,
                document.FirstName,
                document.LastName,
                document.Email,
                document.Password,
                document.Address,
                document.IsRegistrationCompleted);
    }
}