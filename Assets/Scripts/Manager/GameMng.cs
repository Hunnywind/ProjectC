using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameStates;

public class GameMng : Singleton<GameMng>{

    private GameStateMachine<GameMng> _gameStateMachine = new GameStateMachine<GameMng>();

    private void Awake()
    {
        _gameStateMachine.Init(this, new EmptyState());
        DontDestroyOnLoad(gameObject);
    }
    void Start () {
        
        SceneManager.LoadScene("Main");
        
	}
    private void Update()
    {
        _gameStateMachine.Update();
    }
    public void ChangeState(GameState<GameMng> gameState)
    {
        _gameStateMachine.ChangeState(gameState);
    }
    public string GetStageName()
    {
        return _gameStateMachine.GetStateName();
    }
}
