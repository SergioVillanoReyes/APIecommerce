using System;
using Microsoft.AspNetCore.Identity;

namespace APIecommerce.Models;

public class ApplicationUser: IdentityUser
{
    public string? Name { get; set; }
}
