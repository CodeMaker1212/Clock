using System;
using UnityEngine.UI;

namespace Clock
{
    public class WaitingConfirmState : IAlarmViewState
    {
        public WaitingConfirmState(Button confirmButton, Action confirmCallback)
        {
            _confirmButton = confirmButton;
            _confirmCallback += confirmCallback;
        }

        private Button _confirmButton;
        private Action _confirmCallback;

        public void Enter()
        {
            _confirmButton.gameObject.SetActive(true);
            _confirmButton.onClick.AddListener(() => _confirmCallback?.Invoke());
        }

        public void Exit()
        {
            _confirmButton.onClick.RemoveAllListeners();
            _confirmButton.gameObject.SetActive(false);           
        }
    }
}