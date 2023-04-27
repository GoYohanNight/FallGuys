using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource SESource;

    private void Awake()
    {
        BGMSource = GameObject.Find("BackGroundMusic").GetComponent<AudioSource>();
        SESource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
    }

    public void GetBGMVolume(float volume)
    {
        volume = BGMSource.volume;
    }
    public void SetBGMVolume(float volume)
    {
        BGMSource.volume = volume;
    }

    public void GetSEVolume(float volume)
    {
        volume = SESource.volume;
    }
    public void SetSEVolume(float volume)
    {
        SESource.volume = volume;
    }
}