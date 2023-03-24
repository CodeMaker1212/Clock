using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clock
{
    public interface ISecondsCountdownMechanism : IRunnable
    {
        event Action OnSecondPassed;
    }

    public class AsyncSecondsCounter : ISecondsCountdownMechanism
    {
        private CancellationTokenSource _cancellationTokenSource;

        public event Action OnSecondPassed;

        public void Run()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var counting = Countdown(_cancellationTokenSource.Token);
        }

        public void Stop() => _cancellationTokenSource.Cancel();

        private async Task Countdown(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(1000, token);
                OnSecondPassed?.Invoke();
            }
        }      
    }
}