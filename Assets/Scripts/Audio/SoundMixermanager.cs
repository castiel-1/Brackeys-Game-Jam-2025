using UnityEngine;
using UnityEngine.Audio;

public class SoundMixermanager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    public void SetMasterVolume(float volume)
    {
        _audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20f);
    }

    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
    }

    public void SetSoundFXVolume(float volume)
    {
        _audioMixer.SetFloat("soundFXVolume", Mathf.Log10(volume) * 20f);
    }
}
