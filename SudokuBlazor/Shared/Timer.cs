using System.Threading;
using System.Threading.Tasks;
//using System.Timers;

namespace Sudoku.Shared
{
    public sealed class TimerHandler : ITimer
    {
        private static Timer _timer;
        private static readonly object _timerLock = new object();

        public void StartTimer(TimerCallback cb, int milliseconds)
        {
            lock (_timerLock)
            {
                _timer ??= new Timer( cb, null, 0, milliseconds);
            }
        }

        public bool IsTimerRunning()
        {
            lock (_timerLock)
            {
                return _timer != null;
            }
        }

        public void StopTimer()
        {
            lock (_timerLock)
            {
                _timer?.Change(Timeout.Infinite, Timeout.Infinite);
                _timer?.Dispose();
                _timer = null;
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            StopTimer();
            return Task.CompletedTask;
        }
    }
}
