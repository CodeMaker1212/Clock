using System;
using UnityEngine.UI;

namespace Clock
{
    public class WaitingSetupState : IAlarmViewState
    {
        public WaitingSetupState(Text ringTime, Button setupButton, Action setupStartCallback)
        {
            _ringTimeGUI = ringTime;
            _setupButton = setupButton;
            _setupStartCallback += setupStartCallback;
        }

        private Text _ringTimeGUI;
        private Button _setupButton;
        private Action _setupStartCallback;

        public void Enter()
        {
            _ringTimeGUI.text = string.Empty;
            _setupButton.gameObject.SetActive(true);
            _setupButton.onClick.AddListener(() => _setupStartCallback?.Invoke());          
        }

        public void Exit()
        {
            _setupButton.onClick.RemoveAllListeners();
            _setupButton.gameObject.SetActive(false);
        }
    }
}