//----------------------
// <auto-generated>
// </auto-generated>
//----------------------
using System;
using LanguageExt;
using Infusion.Model;

namespace Infusion.Ops
{
  internal class ListOrders<A> : InfusionOp<A>
  {
    public readonly Func<OrderList, InfusionOp<A>> Next;
      public readonly long? ProductId;
      public readonly long? ContactId;
      public readonly string Order;
      public readonly bool Paid;
      public readonly int? Offset;
      public readonly int? Limit;
      public readonly string Until;
      public readonly string Since;
    
    public ListOrders(Func<OrderList, InfusionOp<A>> next, long? productId, long? contactId, string order, bool paid, int? offset, int? limit, string until, string since)
    {
            ProductId = productId;
            ContactId = contactId;
            Order = order;
            Paid = paid;
            Offset = offset;
            Limit = limit;
            Until = until;
            Since = since;
          }
  }
}