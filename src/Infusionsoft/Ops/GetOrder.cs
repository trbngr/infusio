//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using System;
using LanguageExt;
using Infusion.Model;

namespace Infusion.Ops
{
  internal class GetOrder<A> : InfusionOp<A>
  {
    public readonly Func<Order, InfusionOp<A>> Next;
      public readonly long? OrderId;
    
    public GetOrder(Func<Order, InfusionOp<A>> next, long? orderId)
    {
            OrderId = orderId;
          }
  }
}