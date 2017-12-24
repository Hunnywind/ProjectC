using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioLevels : MonoBehaviour {

	public AudioMixer mainMixer;					//Used to hold a reference to the AudioMixer mainMixer
    public Slider musicSlider;
    public Slider sfxSlider;

    public void Start()
    {
    }
    public void Init()
    {
        musicSlider.value = GameMng.GetInstance.LoadGameFloat("musicVol") == -1 ? 0 : GameMng.GetInstance.LoadGameFloat("musicVol");
        sfxSlider.value = GameMng.GetInstance.LoadGameFloat("sfxVol") == -1 ? 0 : GameMng.GetInstance.LoadGameFloat("sfxVol");
        SetMusicLevel(musicSlider.value);
        SetSfxLevel(sfxSlider.value);
    }
    //Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
    public void SetMusicLevel(float musicLvl)
	{
        mainMixer.SetFloat("musicVol", musicLvl);
        GameMng.GetInstance.SaveGame("musicVol", musicLvl);
	}

	//Call this function and pass in the float parameter sfxLevel to set the volume of the AudioMixerGroup SoundFx in mainMixer
	public void SetSfxLevel(float sfxLevel)
	{
		mainMixer.SetFloat("sfxVol", sfxLevel);
        GameMng.GetInstance.SaveGame("sfxVol", sfxLevel);
    }
}
