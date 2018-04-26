namespace Infusio.Auth
{
    public class RefreshToken
    {
        public readonly string Value;
        RefreshToken(string value) => Value = value;
        public static RefreshToken New(string value) => new RefreshToken(value);
        public static implicit operator string(RefreshToken id) => id.Value;
    }
}