using System;
using System.Collections.Generic;

namespace Model;

public class User
{
    public Guid Id{ get; set; }
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Bio { get; set; }
    public DateOnly BirthDay { get; set; }
    public DateOnly CreateAccountDate { get; set; }
    public Image? Avatar { get; set; }
    public Image? Banner { get; set; }
    public required ICollection<User> Following { get; set; }
    public required ICollection<User> Followers { get; set; }
    public required ICollection<Post> Posts { get; set; }
}