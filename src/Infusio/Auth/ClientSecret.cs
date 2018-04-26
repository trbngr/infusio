namespace Infusio.Auth
{
    public class ClientSecret
    {
        public readonly string Value;
        ClientSecret(string value) => Value = value;
        public static ClientSecret New(string value) => new ClientSecret(value);
        public static implicit operator string(ClientSecret id) => id.Value;
    }
}