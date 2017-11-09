using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroCoroutine
{
    List<IEnumerator> _coroutines = new List<IEnumerator>();

    public void AddCoroutine(IEnumerator enumerator)
    {
        _coroutines.Add(enumerator);
    }

    public void Run()
    {
        int i = 0;
        while (i < _coroutines.Count)
        {
            if (!_coroutines[i].MoveNext())
            {
                _coroutines.RemoveAt(i);
                continue;
            }
            i++;
        }
    }
}

public class CoroutineManager : MonoBehaviour
{
    #region Singleton

    private static CoroutineManager _instance;
    public static CoroutineManager instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject();
                go.name = "~CoroutineManager";
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<CoroutineManager>();
            }
            return _instance;
        }
    }

    #endregion

    MicroCoroutine updateMicroCoroutine = new MicroCoroutine();
    MicroCoroutine fixedUpdateMicroCoroutine = new MicroCoroutine();
    MicroCoroutine endOfFrameMicroCoroutine = new MicroCoroutine();

    public static void StartUpdateCoroutine(IEnumerator routine)
    {
        if (_instance == null)
            return;
        _instance.updateMicroCoroutine.AddCoroutine(routine);
    }

    public static void StartFixedUpdateCoroutine(IEnumerator routine)
    {
        if (_instance == null)
            return;
        _instance.fixedUpdateMicroCoroutine.AddCoroutine(routine);
    }

    public static void StartEndOfFrameCoroutine(IEnumerator routine)
    {
        if (_instance == null)
            return;
        _instance.endOfFrameMicroCoroutine.AddCoroutine(routine);
    }

    void Awake()
    {
        StartCoroutine(RunUpdateMicroCoroutine());
        StartCoroutine(RunFixedUpdateMicroCoroutine());
        StartCoroutine(RunEndOfFrameMicroCoroutine());
    }

    IEnumerator RunUpdateMicroCoroutine()
    {
        while (true)
        {
            yield return null;
            updateMicroCoroutine.Run();
        }
    }

    IEnumerator RunFixedUpdateMicroCoroutine()
    {
        var fu = new WaitForFixedUpdate();
        while (true)
        {
            yield return fu;
            fixedUpdateMicroCoroutine.Run();
        }
    }

    IEnumerator RunEndOfFrameMicroCoroutine()
    {
        var eof = new WaitForEndOfFrame();
        while (true)
        {
            yield return eof;
            endOfFrameMicroCoroutine.Run();
        }
    }
}
