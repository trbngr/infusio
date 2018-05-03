using System;
using System.Net;
using LanguageExt;
using LanguageExt.TypeClasses;

namespace Infusio.Http
{
    public class KnownResponse : Record<KnownResponse>
    {
        public readonly int StatusCode;
        public readonly string Description;
        public readonly Type Type;
        public bool IsSuccess => (StatusCode >= 200) && (StatusCode <= 299);

        public KnownResponse(int statusCode, string description, Type type)
        {
            StatusCode = statusCode;
            Description = description;
            Type = type;
        }

        public static KnownResponse For(HttpStatusCode code) => new KnownResponse((int) code, "", null);

        public struct Eq : Eq<KnownResponse>
        {
            public bool Equals(KnownResponse x, KnownResponse y) => x.StatusCode.Equals(y.StatusCode);
            public int GetHashCode(KnownResponse x) => x.StatusCode.GetHashCode();
        }
    }
}