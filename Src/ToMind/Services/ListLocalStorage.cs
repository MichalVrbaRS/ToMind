using Blazored.LocalStorage;

namespace ToMind.Services;

public sealed class ListLocalStorage
{
    private const string LastOpenedListKey = "lastOpenedListGuid";
    private const string RememberMeTokenPrefix = "rememberMeToken:";
    private readonly ILocalStorageService _storage;

    public ListLocalStorage(ILocalStorageService storage)
    {
        _storage = storage;
    }

    public async Task<Guid?> GetLastOpenedListIdAsync()
    {
        var raw = await _storage.GetItemAsync<string>(LastOpenedListKey);
        return Guid.TryParse(raw, out var value) ? value : null;
    }

    public async Task SetLastOpenedListIdAsync(Guid listId)
    {
        await _storage.SetItemAsync(LastOpenedListKey, listId.ToString("D"));
    }

    public async Task ClearLastOpenedListIdAsync()
    {
        await _storage.RemoveItemAsync(LastOpenedListKey);
    }

    public async Task<string?> GetRememberMeTokenAsync(Guid listId)
    {
        return await _storage.GetItemAsync<string>(RememberMeTokenKey(listId));
    }

    public async Task SetRememberMeTokenAsync(Guid listId, string token)
    {
        await _storage.SetItemAsync(RememberMeTokenKey(listId), token);
    }

    public async Task ClearRememberMeTokenAsync(Guid listId)
    {
        await _storage.RemoveItemAsync(RememberMeTokenKey(listId));
    }

    private static string RememberMeTokenKey(Guid listId)
    {
        return $"{RememberMeTokenPrefix}{listId:D}";
    }
}
