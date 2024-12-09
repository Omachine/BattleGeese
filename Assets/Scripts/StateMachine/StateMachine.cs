using UnityEngine;

public abstract class StateMachine
{
    protected IState currentState;

    public void Initialize(IState initialState)
    {
        currentState = initialState;

        currentState.Enter();
    }

    public virtual void HandleInput() => currentState.HandleInput();

    public virtual void Update() => currentState.Update();

    public virtual void PhysicsUpdate() => currentState.PhysicsUpdate();

    public void ChangeState(IState newState)
    {
        if (newState == currentState) return;
        
        currentState.Exit();

        currentState = newState;

        currentState.Enter();
    }
}
