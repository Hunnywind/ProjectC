using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SceneManagement;

public class GPGSMng : Singleton<GPGSMng> {
    public bool bLogin
    {
        get;
        set;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitGPGS();
    }
    public void InitGPGS()
    {
        bLogin = false;
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
    }
    public void LoginGPGS()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate(LoginCallbackGPGS);
        }
    }
    public void LoginCallbackGPGS(bool result)
    {
        bLogin = result;
        if (bLogin)
        {
            SceneManager.LoadScene("Main");
        }
    }
    public void LogoutGPGS()
    {
        if (Social.localUser.authenticated)
        {
            ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
            bLogin = false;
        }
    }
    public string GetNameGPGS()
    {
        if (Social.localUser.authenticated)
            return Social.localUser.userName;
        else
            return null;
    }
    public void OnApplicationQuit()
    {
        GPGSMng.GetInstance.LogoutGPGS();
    }
}

