namespace StateMachineCore
{
    public interface IStateWithArg<TArgs> : IExitableState
    {
        void Enter(TArgs args);
    }
}