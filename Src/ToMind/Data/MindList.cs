namespace ToMind.Data;

public sealed class MindList
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
    public ListType Type { get; set; }
    public string? ProjectName { get; set; }
    public string? PeopleJson { get; set; }
    public string? PasswordHash { get; set; }
    public string? RememberMeTokenHash { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    public ICollection<BoardCard> BoardCards { get; set; } = new List<BoardCard>();
}
