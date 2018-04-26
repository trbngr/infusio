namespace Infusio.Auth
{
    public class AccessCode
    {
        public readonly string Value;
        AccessCode(string value) => Value = value;
        public static AccessCode New(string value) => new AccessCode(value);
        public static implicit operator string(AccessCode id) => id.Value;
    }
}