using System;

namespace Coypu.Timing
{
    public interface IWaiter
    {
        void Wait(TimeSpan duration);
    }
}