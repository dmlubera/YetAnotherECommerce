﻿namespace YetAnotherECommerce.Shared.Abstractions.Mongo
{
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}