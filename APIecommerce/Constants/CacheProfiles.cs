using System;
using Microsoft.AspNetCore.Mvc;

namespace APIecommerce.Constants;

public class CacheProfiles
{
    public const string Default10 = "Default10";

    public const string Default20 = "Default20";

    public static readonly CacheProfile Profille10 = new()
    {
        Duration = 10
    };

    public static readonly CacheProfile Profille20 = new()
    {
        Duration = 20
    };
}
