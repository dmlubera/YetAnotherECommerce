using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using YetAnotherECommerce.Modules.Identity.Core.Entities;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests;

public class FakeUserManager() : UserManager<User>(
    new Mock<IUserStore<User>>().Object,
    new Mock<IOptions<IdentityOptions>>().Object,
    new Mock<IPasswordHasher<User>>().Object,
    Array.Empty<IUserValidator<User>>(), 
    Array.Empty<IPasswordValidator<User>>(),
    new Mock<ILookupNormalizer>().Object,
    new Mock<IdentityErrorDescriber>().Object,
    new Mock<IServiceProvider>().Object,
    new Mock<ILogger<UserManager<User>>>().Object);