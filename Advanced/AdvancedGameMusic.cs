using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdvancedGameMusic : MonoBehaviour
{
    public enum MusicState
    {
        None,
        Chill,
        Danger,
        Battle,
        Victory,
        Death,
        // Add more states as required
    }

    public static AdvancedGameMusic instance;

    public float musicFadeSpeed = 5;
    public float maxVolume = 1;

    private MusicState currentState = MusicState.None;
    private Dictionary<MusicState, AudioSource> stateToMusic = new Dictionary<MusicState, AudioSource>();

    private void Awake()
    {
        instance = this;
        AudioListener.volume = 1;
        maxVolume = PlayerPrefs.GetFloat("MusicVolume", 1);

        // Initialize the state-to-music mapping (assuming AudioSources are attached to this GameObject)
        stateToMusic[MusicState.Chill] = transform.Find("ChillMusic").GetComponent<AudioSource>();
        stateToMusic[MusicState.Danger] = transform.Find("DangerMusic").GetComponent<AudioSource>();
        stateToMusic[MusicState.Battle] = transform.Find("BattleMusic").GetComponent<AudioSource>();
        // ... Add other states and their respective music sources
    }

    public void ChangeMusicState(MusicState newState)
    {
        if (currentState != newState)
        {
            if (currentState != MusicState.None)
            {
                StartCoroutine(FadeOut(stateToMusic[currentState]));
            }

            if (newState != MusicState.None)
            {
                StartCoroutine(FadeIn(stateToMusic[newState]));
            }

            currentState = newState;
        }
    }

    private IEnumerator FadeOut(AudioSource audioSource)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime * musicFadeSpeed;
            yield return null;
        }
        audioSource.Stop();
    }

    private IEnumerator FadeIn(AudioSource audioSource)
    {
        audioSource.Play();
        while (audioSource.volume < maxVolume)
        {
            audioSource.volume += Time.deltaTime * musicFadeSpeed;
            yield return null;
        }
    }
}
