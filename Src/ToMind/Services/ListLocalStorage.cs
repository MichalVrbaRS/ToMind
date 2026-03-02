using Blazored.LocalStorage;

namespace ToMind.Services;

public sealed class ListLocalStorage
{
    private const string LastOpenedListKey = "lastOpenedListGuid";
    private const string RecentOpenedListKey = "recentOpenedListGuids";
    private const string RememberMeTokenPrefix = "rememberMeToken:";
    private const int MaxRecentLists = 10;
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

    public async Task<IReadOnlyList<Guid>> GetRecentOpenedListIdsAsync()
    {
        var raw = await _storage.GetItemAsync<string[]>(RecentOpenedListKey);
        if (raw is null || raw.Length == 0)
        {
            return Array.Empty<Guid>();
        }

        var parsed = new List<Guid>(raw.Length);
        foreach (var value in raw)
        {
            if (Guid.TryParse(value, out var id))
            {
                parsed.Add(id);
            }
        }

        return NormalizeRecentList(parsed);
    }

    public async Task SetLastOpenedListIdAsync(Guid listId)
    {
        await _storage.SetItemAsync(LastOpenedListKey, listId.ToString("D"));
        var current = await GetRecentOpenedListIdsAsync();
        var ordered = new List<Guid>(current.Count + 1) { listId };
        foreach (var id in current)
        {
            if (id != listId)
            {
                ordered.Add(id);
            }
        }
        await SaveRecentOpenedListIdsAsync(ordered);
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

    private static List<Guid> NormalizeRecentList(IEnumerable<Guid> ids)
    {
        var list = new List<Guid>();
        foreach (var id in ids)
        {
            if (id == Guid.Empty || list.Contains(id))
            {
                continue;
            }

            list.Add(id);
            if (list.Count == MaxRecentLists)
            {
                break;
            }
        }
        return list;
    }

    private async Task SaveRecentOpenedListIdsAsync(IEnumerable<Guid> ids)
    {
        var normalized = NormalizeRecentList(ids);
        var payload = normalized.Select(id => id.ToString("D")).ToArray();
        await _storage.SetItemAsync(RecentOpenedListKey, payload);
    }
}
