using UnityEngine;
using System.Collections;
using System.Linq;

public class AdvancedGameMusic : MonoBehaviour
{
    [System.Serializable]
    public struct MusicEntry
    {
        public MusicState state;
        public AudioClip clip;
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

    public static AdvancedGameMusic instance;

    public float musicFadeSpeed = 5;
    public float maxVolume = 1;
    public MusicEntry[] musicEntries;

    private AudioSource activeAudioSource;
    private AudioSource inactiveAudioSource;
    private MusicState currentState = MusicState.None;

    private void Awake()
    {
        instance = this;
        AudioListener.volume = 1;
        maxVolume = PlayerPrefs.GetFloat("MusicVolume", 1);

        // Initialize audio sources
        AudioSource[] sources = GetComponents<AudioSource>();
        activeAudioSource = sources[0];
        inactiveAudioSource = sources[1];
    }

    public static void ChangeState(MusicState state)
    {
        if (instance)
        {
            instance.ChangeMusicState(state);
        }
    }
    
    public void ChangeMusicState(MusicState newState)
    {
        if (currentState != newState)
        {
            AudioClip newClip = musicEntries.FirstOrDefault(entry => entry.state == newState).clip;
            
            if (newClip)
            {
                inactiveAudioSource.clip = newClip;
                StartCoroutine(FadeTransition());
            }
            
            currentState = newState;
        }
    }

    private IEnumerator FadeTransition()
    {
        inactiveAudioSource.Play();
        
        while (activeAudioSource.volume > 0)
        {
            activeAudioSource.volume -= Time.deltaTime * musicFadeSpeed;
            inactiveAudioSource.volume += Time.deltaTime * musicFadeSpeed;
            yield return null;
        }
        
        activeAudioSource.Stop();

        // Swap active and inactive sources
        var temp = activeAudioSource;
        
        activeAudioSource = inactiveAudioSource;
        inactiveAudioSource = temp;
        inactiveAudioSource.volume = 0; // Reset volume for the next transition
    }
}
