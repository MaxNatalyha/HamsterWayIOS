using Infrastructure;
using UnityEngine;
using Zenject;

public class GameStartup : MonoBehaviour
{
    private GlobalStateMachine _stateMachine;

    [Inject]
    public void Construct(GlobalStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void Awake() => StartStateMachine();

    private void StartStateMachine()
    {
        _stateMachine.EnterState<LoadingState>();
    }
    
    private void OnApplicationQuit() => _stateMachine.EnterState<QuitState>();
}