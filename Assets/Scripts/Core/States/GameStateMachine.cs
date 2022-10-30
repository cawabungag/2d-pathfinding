using System;
using System.Collections.Generic;
using Core;
using Infrastructure;
using Infrastructure.States;
using States;

public class GameStateMachine
{
	private readonly Dictionary<Type, IExitableState> _states = new();
	private IExitableState _activeState;

	public GameStateMachine(SceneLoaderService sceneLoaderService, ServiceLocator services, CanvasRoot canvasRoot)
	{
		var boostrapState = new BoostrapState(this, sceneLoaderService, services);
		var startState = new StartState(this, services, canvasRoot, sceneLoaderService);
		var gameState = new GameState();
			
		_states.Add(typeof(BoostrapState), boostrapState);
		_states.Add(typeof(StartState), startState);
		_states.Add(typeof(GameState), gameState);
	}

	public void Enter<TState>() where TState : class, IState
	{
		var state = ChangeState<TState>();
		state.Enter();
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