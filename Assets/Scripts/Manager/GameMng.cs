using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : Singleton<GameMng>{
    
	void Start () {
        SceneManager.LoadScene("Main");
        DontDestroyOnLoad(gameObject);
	}
	
}
