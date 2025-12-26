using System.Threading;

namespace Tululoo.Core.Managers
{
    public class UidProvider
    {
        private int _counter;

        public UidProvider(int start = 1) => _counter = start;

        public int Next() => Interlocked.Increment(ref _counter);
    }
}