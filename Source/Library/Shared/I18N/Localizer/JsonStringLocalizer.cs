using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace I18N.Localizer;

public class JsonStringLocalizer : IStringLocalizer
{
    private readonly IDistributedCache _distributedCache;
    private readonly JsonSerializer _jsonSerializer = new JsonSerializer();

    public JsonStringLocalizer(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public LocalizedString this[string name]
    {
        get
        {
            string value = GetLocalizedString(name);

            return new LocalizedString(name, value ?? name, value == null);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var theActualValue = this[name];

            return !theActualValue.ResourceNotFound
                ? new LocalizedString(name, string.Format(theActualValue.Value, arguments), false)
                : theActualValue;
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        string filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";

        using (var str = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (var sr = new StreamReader(str))
        using (var reader = new JsonTextReader(sr))
        {
            while (reader.Read())
            {
                if (reader.TokenType != JsonToken.PropertyName)
                    continue;

                string? key = reader.Value as string;

                reader.Read();

                if (reader.TokenType == JsonToken.StartObject)
                    continue;

                string? value = _jsonSerializer.Deserialize<string?>(reader);

                yield return new LocalizedString(key, value, false);
            }
        }
    }

    private string? GetJsonValue(string propertyName, string filePath)
    {
        if (propertyName == null) return default;
        if (filePath == null) return default;

        using (var str = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (var sReader = new StreamReader(str))
        using (var reader = new JsonTextReader(sReader))
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Value as string == propertyName)
                {
                    reader.Read();
                    return _jsonSerializer.Deserialize<string>(reader);
                }
            }

            return default;
        }
    }

    private string GetLocalizedString(string key)
    {
        string relativeFilePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
        string fullFilePath = Path.GetFullPath(relativeFilePath);

        if (File.Exists(fullFilePath))
        {
            string cacheKey = $"locale_{Thread.CurrentThread.CurrentCulture.Name}_{key}";
            string cacheValue = _distributedCache.GetString(cacheKey);

            if (!string.IsNullOrEmpty(cacheValue)) return cacheValue;

            string result = GetJsonValue(key, filePath: Path.GetFullPath(relativeFilePath));

            if (!string.IsNullOrEmpty(result)) _distributedCache.SetString(cacheKey, result);

            return result;
        }

        return default;

    }
}
