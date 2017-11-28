using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMng : Singleton<SoundMng> {

    public AudioSource BGMSource;
    public AudioClip[] sceneBGM;

    public AudioSource[] SFXSource;
    public AudioClip[] soundClip;

    private bool isSEPlay = true;
    public bool IsSEplay { set { isSEPlay = value; } }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void PlayBGM(int id)
    {
        if (id < 0 || id >= sceneBGM.Length)
            return;
        BGMSource.Stop();
        BGMSource.clip = sceneBGM[id];
        BGMSource.Play();
    }
    public void StopBGM()
    {
        BGMSource.Stop();
    }
    public void Play(int id)
    {
        if (!isSEPlay) return;
        if (id < 0 || id >= soundClip.Length)
            return;
        for (int i = 0; i < SFXSource.Length; i++)
        {
            if (!SFXSource[i].isPlaying)
            {
                SFXSource[i].PlayOneShot(soundClip[id]);
                return;
            }
        }
        SFXSource[0].PlayOneShot(soundClip[id]);
    }
}
