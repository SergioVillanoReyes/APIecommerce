using System;
using System.ComponentModel.DataAnnotations;

namespace APIecommerce.Models;

public class User
{
    [Key]
    public int id { get; set; }

    public string? Name { get; set; }

    public string Username { get; set; } = string.Empty;

    public string? Password { get; set; }

    public string? Role { get; set; }
}
