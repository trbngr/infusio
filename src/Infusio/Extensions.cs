using System;
using System.Threading.Tasks;
using LanguageExt;

namespace Infusionsoft
{
    public static class Extensions
    {
        public static async Task<Unit> Lift(this Task task)
        {
            await task;
            return Unit.Default;
        }

        public static Unit LiftA(Action action)
        {
            action();
            return Unit.Default;
        }
    }
}