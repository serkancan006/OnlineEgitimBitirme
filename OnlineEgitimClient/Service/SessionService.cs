using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace OnlineEgitimClient.Service
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetValue<T>(string key, T value, TimeSpan? expireTime = null)
        {
            _httpContextAccessor?.HttpContext?.Session.Set(key, JsonSerializer.SerializeToUtf8Bytes(value));

            if (expireTime.HasValue)
            {
                var expireTimeString = DateTime.Now.Add(expireTime.Value).ToString();
                _httpContextAccessor?.HttpContext?.Session.SetString(key + "_Expires", expireTimeString);
            }
        }

        public T? GetValue<T>(string key)
        {
            var data = _httpContextAccessor?.HttpContext?.Session.Get(key);
            if (data != null)
            {
                var expires = _httpContextAccessor?.HttpContext?.Session.GetString(key + "_Expires");
                if (expires != null)
                {
                    var expireTime = DateTime.Parse(expires);
                    if (DateTime.Now > expireTime)
                    {
                        _httpContextAccessor?.HttpContext?.Session.Remove(key);
                        _httpContextAccessor?.HttpContext?.Session.Remove(key + "_Expires");
                        return default(T);
                    }
                }

                return JsonSerializer.Deserialize<T>(data);
            }

            return default(T);
        }
    }
}