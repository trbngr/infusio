using System;
using Infusion.Client;

// ReSharper disable InconsistentNaming

namespace Infusion
{
    public abstract class InfusionOp<A>
    {
        internal class Return : InfusionOp<A>
        {
            public readonly A Value;
            public Return(A value) => Value = value;
        }
    }
}