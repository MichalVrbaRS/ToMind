namespace ToMind.Services;

public sealed class TopBarState
{
    public event Action? Changed;

    public string Title { get; private set; } = "ToMind";
    public bool ShowCreateButton { get; private set; }
    public bool ShowShareButton { get; private set; }
    public string? ShareUrl { get; private set; }

    public void Set(string title, bool showCreateButton, bool showShareButton = false, string? shareUrl = null)
    {
        Title = string.IsNullOrWhiteSpace(title) ? "ToMind" : title;
        ShowCreateButton = showCreateButton;
        ShowShareButton = showShareButton;
        ShareUrl = showShareButton ? shareUrl : null;
        Changed?.Invoke();
    }
}
