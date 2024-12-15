using System;

namespace Model;

public record Image(
    Guid Id,
    byte[] Data
);