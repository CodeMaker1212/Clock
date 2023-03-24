namespace Clock
{
    public class StateMachine
    {
        private IState _currentState;

        public StateMachine(IState defaultState)
        {
            _currentState = defaultState;
            _currentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            if (newState == _currentState) return;

            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }

    public interface IState
    {
        void Enter();

        void Exit();
    }

    public interface IAlarmViewState : IState { }
}