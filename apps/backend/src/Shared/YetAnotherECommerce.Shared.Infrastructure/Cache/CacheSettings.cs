using System;
using System.Collections.Generic;

namespace YetAnotherECommerce.Shared.Infrastructure.Cache;

public class CacheSettings
{
    public static string SectionName => "CacheSettings";
    public IReadOnlyDictionary<string, TimeSpan> Expirations { get; init; }
}