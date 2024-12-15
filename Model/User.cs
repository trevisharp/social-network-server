using System;
using System.Collections.Generic;

namespace Model;

public record User(
    Guid Id,
    string Name,
    string Username,
    string Bio,
    DateOnly BirthDay,
    DateOnly CreateAccountDate,
    Image? Avatar,
    Image? Banner,
    ICollection<User> Following,
    ICollection<User> Followers,
    ICollection<Post> Posts
);