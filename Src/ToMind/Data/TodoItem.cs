namespace ToMind.Data;

public sealed class TodoItem
{
    public int Id { get; set; }
    public Guid ListId { get; set; }
    public MindList List { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    public int SortOrder { get; set; }
}
