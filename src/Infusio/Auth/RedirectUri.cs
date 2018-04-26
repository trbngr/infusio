namespace Infusio.Auth
{
    public class RedirectUri
    {
        public readonly string Value;
        RedirectUri(string value) => Value = value;
        public static RedirectUri New(string value) => new RedirectUri(value);
        public static implicit operator string(RedirectUri id) => id.Value;
    }
}