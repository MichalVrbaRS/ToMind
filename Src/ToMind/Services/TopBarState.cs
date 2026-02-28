namespace ToMind.Services;

public sealed class TopBarState
{
    public event Action? Changed;

    public string Title { get; private set; } = "ToMind";
    public string? Description { get; private set; }
    public bool ShowCreateButton { get; private set; }
    public bool ShowShareButton { get; private set; }
    public string? ShareUrl { get; private set; }

    public void Set(string title, bool showCreateButton, bool showShareButton = false, string? shareUrl = null, string? description = null)
    {
        Title = string.IsNullOrWhiteSpace(title) ? "ToMind" : title;
        ShowCreateButton = showCreateButton;
        ShowShareButton = showShareButton;
        ShareUrl = showShareButton ? shareUrl : null;
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        Changed?.Invoke();
    }
}
