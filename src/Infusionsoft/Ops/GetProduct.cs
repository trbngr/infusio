//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using System;
using LanguageExt;
using Infusion.Model;

namespace Infusion.Ops
{
  internal class GetProduct<A> : InfusionOp<A>
  {
    public readonly Func<Product, InfusionOp<A>> Next;
      public readonly long? ProductId;
    
    public GetProduct(Func<Product, InfusionOp<A>> next, long? productId)
    {
            ProductId = productId;
          }
  }
}