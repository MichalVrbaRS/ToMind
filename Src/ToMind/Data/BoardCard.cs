namespace ToMind.Data;

public sealed class BoardCard
{
    public int Id { get; set; }
    public Guid ListId { get; set; }
    public MindList List { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ProjectName { get; set; }
    public string? AssigneeName { get; set; }
    public BoardColumn Column { get; set; }
    public int SortOrder { get; set; }
}
