using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameStates;

public class GameMng : Singleton<GameMng>{

    private GameStateMachine<GameMng> _gameStateMachine = new GameStateMachine<GameMng>();

    private void Awake()
    {
        EncryptedPlayerPrefs.keys = new string[5];
        EncryptedPlayerPrefs.keys[0] = "82Emssnq";
        EncryptedPlayerPrefs.keys[1] = "SOPI23sn";
        EncryptedPlayerPrefs.keys[2] = "olkpoww";
        EncryptedPlayerPrefs.keys[3] = "pplzmql";
        EncryptedPlayerPrefs.keys[4] = "wrrwlqs";
        _gameStateMachine.Init(this, new EmptyState());
        DontDestroyOnLoad(gameObject);
    }
    void Start () {
        
#if UNITY_EDITOR
        SceneManager.LoadScene("Main");
#elif UNITY_ANDROID
        SceneManager.LoadScene("Title");
#endif
    }
    public void Touch(float x, float y)
    {
        if (StageMng.GetInstance != null)
            StageMng.GetInstance.Touch();
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

    public void SaveGame(string key, int value)
    {
        EncryptedPlayerPrefs.SetInt(key, value);
    }
    public void SaveGame(string key, float value)
    {
        EncryptedPlayerPrefs.SetFloat(key, value);
    }
    public int LoadGame(string key)
    {
        return EncryptedPlayerPrefs.GetInt(key, 0);
    }
    public float LoadGameFloat(string key)
    {
        return EncryptedPlayerPrefs.GetFloat(key, -1);
    }
}
