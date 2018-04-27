using System;
using System.IO;
using Infusio.Auth;
using LanguageExt;
using Newtonsoft.Json;

namespace Demo
{
    public static class TokenCache
    {
        public static OptionAsync<AuthorizationInfo> AuthorizationInfoFromCache() => (
            from info in LoadFromCache()
            from valid in EnsureLifetime(info)
            select valid
        ).ToAsync();

        static Option<AuthorizationInfo> LoadFromCache() => File.Exists("auth.json")
            ? JsonConvert.DeserializeObject<AuthorizationInfo>(File.ReadAllText("auth.json"))
            : null;

        static Option<AuthorizationInfo> EnsureLifetime(AuthorizationInfo info)
        {
            var (_, _, expiration) = info;
            return expiration > DateTime.UtcNow ? info : null;
        }

        public static AuthorizationInfo CacheAuthorizationInfo(AuthorizationInfo info)
        {
            File.WriteAllText("auth.json", JsonConvert.SerializeObject(info, Formatting.Indented));
            return info;
        }
    }
}