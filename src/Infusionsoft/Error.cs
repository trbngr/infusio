using System.Runtime.Serialization;
using LanguageExt;
// ReSharper disable ClassNeverInstantiated.Global

namespace Infusionsoft
{
    public class Error : NewType<Error, string>
    {
        public Error(string value) : base(value) { }
        public Error(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}