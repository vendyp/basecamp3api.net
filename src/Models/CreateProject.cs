﻿namespace Basecamp3Api.Models;

public record CreateProject
{
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("description")] public string? Description { get; set; }
}