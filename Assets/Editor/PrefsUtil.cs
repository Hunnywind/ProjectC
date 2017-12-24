#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class PrefsUtil : EditorWindow
{
    private static GUIStyle labelStyle;
    private static Dictionary<string, object> currentPrefs;

    [MenuItem("Window/Motiga/Prefs Util")]
    public static void ShowWindow()
    {
        labelStyle = GUIStyle.none;
        labelStyle.normal.textColor = Color.white;
        labelStyle.wordWrap = true;

        currentPrefs = GetCurrentPrefs();

        EditorWindow.GetWindow(typeof(PrefsUtil));
    }

    private static Dictionary<string, object> GetCurrentPrefs()
    {
        if (Application.platform == RuntimePlatform.OSXEditor)
        {
            return GetMacPrefs();
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            return GetWindowsPrefs();
        }
        return null;
    }

    private static Dictionary<string, object> GetMacPrefs()
    {
        string prefsFileName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)
            + "/Library/Preferences/unity."
            + PlayerSettings.companyName + "."
            + PlayerSettings.productName + ".plist";

        Dictionary<string, object> plist;
        try
        {
            plist = Plist.createDictionaryFromBinaryFile(prefsFileName);
        }
        catch (System.Exception eouter)
        {
            Debug.Log("Couldn't read binary prefs: " + eouter.ToString());
            try
            {
                plist = Plist.createDictionaryFromXmlFile(prefsFileName);
            }
            catch (System.Exception einner)
            {
                Debug.Log("Couldn't read xml prefs: " + einner.ToString());
                return null;
            }
        }

        return plist;
    }

    private static Dictionary<string, object> GetWindowsPrefs()
    {
        return new Dictionary<string, object>();
    }

    public void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("This will clear all of your current game prefs to allow a fresh start",
            labelStyle,
            GUILayout.MaxWidth(200));

        if (GUILayout.Button("Clear Prefs", GUILayout.MaxWidth(200)))
        {
            PlayerPrefs.DeleteAll();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        foreach (KeyValuePair<string, object> item in currentPrefs)
        {
            EditorGUILayout.LabelField(item.Key.ToString(), item.Value.ToString());
        }
    }
}
#endif