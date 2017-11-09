using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEditor;
using System.Collections;

class EditorScrips : EditorWindow
{

    [MenuItem("Play/PlayMe _%h")]
    public static void RunMainScene()
    {
        EditorApplication.OpenScene("Assets/Scenes/Intro.unity");
        EditorApplication.isPlaying = true;
    }
}
