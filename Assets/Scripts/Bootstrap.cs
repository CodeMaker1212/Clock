using UnityEngine;
using UnityEngine.SceneManagement;

namespace Clock
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ClockView _clockView;
        [SerializeField] private AlarmView _alarmView;

        private ClockController _controller;

        private void Start()
        {
            CurrentTimeFetcher currentTimeFetcher = new CurrentTimeFetcher();
            ISecondsCountdownMechanism secondsCountdownMechanism = new AsyncSecondsCounter();
            IClock clock = new Clock(secondsCountdownMechanism);
            IClock selfCorrectingClock = new TimeCorrector(clock, currentTimeFetcher);
            Alarm alarm = new Alarm(clock);
            _controller = new ClockController(selfCorrectingClock, _clockView, alarm, _alarmView);

            ServiceLocator.RegisterService<ITimeService>(clock);

            SceneManager.LoadScene(1);
        }

        private void OnApplicationQuit() => _controller.DisableClock();
    }
}