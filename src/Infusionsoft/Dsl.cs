using System;

// ReSharper disable InconsistentNaming

namespace Infusionsoft
{
    public static class Dsl
    {
        public static Op<A> Return<A>(A value) =>
            new Op<A>.Return(value);

        public static Op<AccountProfile> GetAccountProfile() =>
            new Op<AccountProfile>.GetAccountProfile(Return);

        public static Op<AccountProfile> UpdateAccountProfile(AccountProfile profile) =>
            new Op<AccountProfile>.UpdateAccountProfile(profile, Return);

        //icky
        public static T With<T>(this T self, Action<T> act)
        {
            act(self);
            return self;
        }
    }
}