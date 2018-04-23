//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using System;
using LanguageExt;
using Infusion.Model;

namespace Infusion.Ops
{
  internal class GetFile<A> : InfusionOp<A>
  {
    public readonly Func<FileInformation, InfusionOp<A>> Next;
      public readonly long? FileId;
      public readonly Lst<string> OptionalProperties;
    
    public GetFile(Func<FileInformation, InfusionOp<A>> next, long? fileId, Lst<string> optionalProperties)
    {
            FileId = fileId;
            OptionalProperties = optionalProperties;
          }
  }
}