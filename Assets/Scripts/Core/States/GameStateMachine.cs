using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.States;

public class GameStateMachine
{
	private readonly Dictionary<Type, IExitableState> _states = new();
	private IExitableState _activeState;

	public GameStateMachine(SceneLoader sceneLoader, ServiceLocator services)
	{
		var boostrapState = new BoostrapState(this, sceneLoader, services);
		_states.Add(typeof(BoostrapState), boostrapState);
	}

	public void Enter<TState>() where TState : class, IState
	{
		var state = ChangeState<TState>();
		state.Enter();
	}

	public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
	{
		var state = ChangeState<TState>();
		state.Enter(payload);
	}

	public void Update()
	{
		_activeState?.Update();
	}

	private TState ChangeState<TState>() where TState : class, IExitableState
	{
		_activeState?.Exit();

		var state = GetState<TState>();
		_activeState = state;

		return state;
	}

	private TState GetState<TState>() where TState : class, IExitableState =>
		_states[typeof(TState)] as TState;
}