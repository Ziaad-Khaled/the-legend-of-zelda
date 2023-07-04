using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource sfxPlayer;
    [SerializeField] AudioSource pausePlayer;
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioSource oneTimeMusicPlayer;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        pausePlayer.Stop();
        loadVolume();
    }

    void loadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1.0f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1.0f);
        mixer.SetFloat(VolumeOptions.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeOptions.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }

    public void changeMusic(AudioClip musicClip)
    {
        musicPlayer.clip = musicClip;
        musicPlayer.Play();
    }

    public void onPauseAudioHandler()
    {
        musicPlayer.Pause();
        pausePlayer.Play();
    }

    public void onResumeAudioHandler()
    {
        pausePlayer.Stop();
        musicPlayer.Play();
    }

    public void PlaySFX(AudioClip soundSFX)
    {
        sfxPlayer.PlayOneShot(soundSFX);
    }

    public void PlayMusicOneTime(AudioClip clip)
    {
        oneTimeMusicPlayer.clip = clip;
        oneTimeMusicPlayer.Play();
    }

    public void stopMainMusic()
    {
        musicPlayer.Stop();
    }
}
