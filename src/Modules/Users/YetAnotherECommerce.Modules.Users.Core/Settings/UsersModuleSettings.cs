﻿using YetAnotherECommerce.Shared.Abstractions.Modules;

namespace YetAnotherECommerce.Modules.Users.Core.Settings
{
    public class UsersModuleSettings : IModuleSettings
    {
        public string DatabaseName { get; set; }
    }
}