using System.Threading;

namespace Sudoku.Shared
{
    public interface ITimer
    {
        void StartTimer(TimerCallback cb, int milliseconds);
        void StopTimer();
        bool IsTimerRunning();
    }
}
