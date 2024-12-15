using System;
using System.Collections.Generic;

namespace Model;

public record Post
{
    public Guid Id { get; set; }
    public DateTime PostDate { get; set; }
    public required User Author { get; set; }
    public required string Text { get; set; }
    public Image? Image { get; set; }
    public Post? Parent { get; set; }
    public required ICollection<Post> Posts { get; set; }
}