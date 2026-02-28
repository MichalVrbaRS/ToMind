namespace ToMind.Services;

public sealed class TopBarState
{
    public event Action? Changed;

    public string Title { get; private set; } = "ToMind";
    public bool ShowCreateButton { get; private set; }

    public void Set(string title, bool showCreateButton)
    {
        Title = string.IsNullOrWhiteSpace(title) ? "ToMind" : title;
        ShowCreateButton = showCreateButton;
        Changed?.Invoke();
    }
}
