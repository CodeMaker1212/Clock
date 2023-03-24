using System;
using UnityEngine;
using UnityEngine.UI;

namespace Clock
{
    public class AlarmView : MonoBehaviour, IAlarmView
    {
        [SerializeField] private Button _setupButton;
        [SerializeField] private Button _confirmSetupButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private Text _ringTimeText;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Animation _animation;

        private StateMachine _stateMachine;
        private WaitingSetupState _waitingSetupState;
        private WaitingConfirmState _waitingConfirmState;
        private WaitingRingState _waitingRingState;
        private RingState _ringState;

        public event Action OnSetupClick;
        public event Action OnConfirmRingTimeClick;     
        public event Action OnTurnOffRingClick;
        public event Action OnResetRingClick;

        private void Awake()
        {
            _waitingSetupState = new WaitingSetupState(_ringTimeText, _setupButton, () => OnSetupClick?.Invoke());
            _waitingConfirmState = new WaitingConfirmState(_confirmSetupButton,() => OnConfirmRingTimeClick?.Invoke());
            _waitingRingState = new WaitingRingState(_setupButton, _resetButton, () => OnResetRingClick?.Invoke());
            _ringState = new RingState(_animation, _audioSource, _ringTimeText, _setupButton, () => OnTurnOffRingClick?.Invoke());

            _stateMachine = new StateMachine(_waitingSetupState);
        }

        public void WaitForSetupStart() => _stateMachine.ChangeState(_waitingSetupState);

        public void WaitForSetupConfirming() => _stateMachine.ChangeState(_waitingConfirmState);

        public void WaitForRing(Time ringTime)
        {
            _ringTimeText.text =
              $"{ringTime.Hours:00}:{ringTime.Minutes:00}:{ringTime.Seconds:00}";
            _stateMachine.ChangeState(_waitingRingState);
        }

        public void Ring() => _stateMachine.ChangeState(_ringState);

        public void StopRing() => _stateMachine.ChangeState(_waitingSetupState);
    }
}