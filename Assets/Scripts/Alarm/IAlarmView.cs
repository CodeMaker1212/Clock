using System;

namespace Clock
{
    public interface IAlarmView
    {
        event Action OnSetupClick;
        event Action OnConfirmRingTimeClick;
        event Action OnTurnOffRingClick;
        event Action OnResetRingClick;

        void WaitForSetupStart();

        void WaitForSetupConfirming();

        void WaitForRing(Time ringTime);

        void Ring();

        void StopRing();
    }
}