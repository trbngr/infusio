//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using System;
using LanguageExt;
using Infusion.Model;

namespace Infusion.Ops
{
  internal class DeleteEmails<A> : InfusionOp<A>
  {
    public readonly Func<Unit, InfusionOp<A>> Next;
      public readonly SetOfIds EmailIds;
    
    public DeleteEmails(Func<Unit, InfusionOp<A>> next, SetOfIds emailIds)
    {
            EmailIds = emailIds;
          }
  }
}