namespace Infusio.Auth
{
    public class ClientId
    {
        public readonly string Value;
        ClientId(string value) => Value = value;
        public static ClientId New(string value) => new ClientId(value);
        public static implicit operator string(ClientId id) => id.Value;
    }
}