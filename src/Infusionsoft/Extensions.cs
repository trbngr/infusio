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
    }
}