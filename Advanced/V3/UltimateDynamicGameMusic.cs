using UnityEngine;
using System.Collections;
using System.Linq;

public class UltimateDynamicGameMusic : MonoBehaviour
{
    [System.Serializable]
    public struct MusicEntry
    {
        public MusicState state;
        public AudioClip clip;
        public float weight; // Higher value means higher priority
    }

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

    public static UltimateDynamicGameMusic instance;

    [Header("Music Configuration")]
    public MusicState defaultState = MusicState.Chill;
    public float defaultFadeDuration = 5f;
    public MusicEntry[] musicEntries;

    [Header("Volume Configuration")]
    public float musicFadeSpeed = 5;
    public float maxVolume = 1;

    private AudioSource activeAudioSource;
    private AudioSource inactiveAudioSource;
    private MusicState currentState;

    private void Awake()
    {
        instance = this;
        AudioListener.volume = 1;
        maxVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        currentState = defaultState;

        AudioSource[] sources = GetComponents<AudioSource>();
        activeAudioSource = sources[0];
        inactiveAudioSource = sources[1];
    }

    public void ChangeMusicState(MusicState newState, float fadeDuration = -1f)
    {
        if (fadeDuration < 0)
            fadeDuration = defaultFadeDuration;

        if (currentState != newState)
        {
            AudioClip newClip = GetClipByState(newState);
            if (newClip)
            {
                inactiveAudioSource.clip = newClip;
                StartCoroutine(FadeTransition(fadeDuration));
            }
            currentState = newState;
        }
    }

    private AudioClip GetClipByState(MusicState state)
    {
        return musicEntries.FirstOrDefault(entry => entry.state == state).clip;
    }

    private IEnumerator FadeTransition(float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            activeAudioSource.volume = Mathf.Lerp(maxVolume, 0, t);
            inactiveAudioSource.volume = Mathf.Lerp(0, maxVolume, t);
            yield return null;
        }
        activeAudioSource.Stop();

        var temp = activeAudioSource;
        activeAudioSource = inactiveAudioSource;
        inactiveAudioSource = temp;
        inactiveAudioSource.volume = 0;
    }

    public static void PlayState(MusicState state)
    {
        if (instance)
        {
            instance.ChangeMusicState(state);
        }
    }

    public void ReturnToDefault()
    {
        ChangeMusicState(defaultState);
    }

    public void SetStateWeight(MusicState state, float weight)
    {
        var entry = musicEntries.FirstOrDefault(e => e.state == state);
        if (entry != null)
        {
            entry.weight = weight;
        }
    }
}
