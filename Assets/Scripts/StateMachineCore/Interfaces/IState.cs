namespace StateMachineCore
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}