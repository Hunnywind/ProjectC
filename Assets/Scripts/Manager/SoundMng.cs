using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMng : Singleton<SoundMng> {

    public AudioSource BGMSource;
    public AudioClip[] sceneBGM;

    public AudioSource[] SFXSource;
    public AudioClip[] soundClip;

    public AudioMixerSnapshot volumeDown;           //Reference to Audio mixer snapshot in which the master volume of main mixer is turned down
    public AudioMixerSnapshot volumeUp;				//Reference to Audio mixer snapshot in which the master volume of main mixer is turned up

    public AudioMixer mixer;

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
    public void PauseBGM()
    {
        BGMSource.Pause();
    }
    public void UnpauseBGM()
    {
        BGMSource.UnPause();
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
