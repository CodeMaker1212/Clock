using System;
using UnityEngine.UI;

namespace Clock
{
    public class WaitingRingState : IAlarmViewState
    {
        public WaitingRingState(Button mainButton, Button resetButton, Action resetCallback)
        {
            _mainButton = mainButton;
            _resetButton = resetButton;
            _resetCallback += resetCallback;
        }

        private Button _mainButton;
        private Button _resetButton;
        private Action _resetCallback;

        public void Enter()
        {
            _mainButton.gameObject.SetActive(true);
            _resetButton.gameObject.SetActive(true);
            _resetButton.onClick.AddListener(() => _resetCallback?.Invoke());
        }

        public void Exit()
        {         
            _resetButton.onClick.RemoveAllListeners();
            _resetButton.gameObject.SetActive(false);
        }
    }
}