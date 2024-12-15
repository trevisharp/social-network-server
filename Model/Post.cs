using System;
using System.Collections.Generic;

namespace Model;

public record Post(
    Guid Id,
    DateTime PostDate,
    User Author,
    string Text,
    Image? Image,
    Post? Parent,
    ICollection<Post> Posts
);