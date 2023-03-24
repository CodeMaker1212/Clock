using System;
using UnityEngine;
using UnityEngine.UI;

namespace Clock
{
    public class RingState : IAlarmViewState
    {
        public RingState(Animation animation,
                         AudioSource audioSource,
                         Text ringTimeGUI,
                         Button ringTurnOffButton,
                         Action turnOffCallback)
        {
            _animation = animation;
            _audioSource = audioSource;
            _ringTimeGUI = ringTimeGUI;
            _ringTurnOffButton = ringTurnOffButton;
            _turnOffCallback += turnOffCallback;
        }

        private Animation _animation;
        private AudioSource _audioSource;
        private Text _ringTimeGUI;
        private Button _ringTurnOffButton;
        private Action _turnOffCallback;

        public void Enter()
        {
            _audioSource.Play();
            _animation.Play();
            _ringTimeGUI.text = string.Empty;
            _ringTurnOffButton.gameObject.SetActive(true);
            _ringTurnOffButton.onClick.AddListener(() => _turnOffCallback?.Invoke());
        }

        public void Exit()
        {
            _audioSource.Stop();
            _animation.Stop();
            _ringTurnOffButton.onClick.RemoveAllListeners();
            _ringTurnOffButton.gameObject.SetActive(false);           
        }
    }
}