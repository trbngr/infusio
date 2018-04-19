using System;

namespace Infusionsoft
{
    public abstract class Op<A>
    {
        public class Return : Op<A>
        {
            public readonly A Value;
            public Return(A value) => Value = value;
        }

        public class GetAccountProfile : Op<A>
        {
            public Func<AccountProfile, Op<A>> Next { get; }
            public GetAccountProfile(Func<AccountProfile, Op<A>> next) => Next = next;
        }

        public class UpdateAccountProfile : Op<A>
        {
            public AccountProfile Profile { get; }
            public Func<AccountProfile, Op<A>> Next { get; }

            public UpdateAccountProfile(AccountProfile profile, Func<AccountProfile, Op<A>> next)
            {
                Profile = profile;
                Next    = next;
            }
        }
    }

    public static class Op
    {
        static Op<B> Bind<A, B>(this Op<A> ma, Func<A, Op<B>> fn) =>
            ma is Op<A>.Return rt                 ? fn(rt.Value) :
            ma is Op<A>.GetAccountProfile gap     ? new Op<B>.GetAccountProfile(x => gap.Next(x).Bind(fn)) :
            ma is Op<A>.UpdateAccountProfile upap ? new Op<B>.UpdateAccountProfile(upap.Profile, x => upap.Next(x).Bind(fn)) as Op<B> :
                                                     throw new NotSupportedException();

        public static Op<B> Map<A, B>(this Op<A> ma, Func<A, B> fn) =>
            ma.Bind(a => Dsl.Return(fn(a)));

        public static Op<B> Select<A, B>(this Op<A> ma, Func<A, B> fn) =>
            ma.Bind(a => Dsl.Return(fn(a)));

        public static Op<C> SelectMany<A, B, C>(this Op<A> ma, Func<A, Op<B>> bind, Func<A, B, C> project) =>
            ma.Bind(a => bind(a).Select(b => project(a, b)));
    }
}