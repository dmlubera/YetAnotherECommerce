﻿using System;
using YetAnotherECommerce.Shared.Abstractions.Events;

namespace YetAnotherECommerce.Modules.Identity.Messages.Events
{
    public class EmailChanged : IEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }

        public EmailChanged(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}