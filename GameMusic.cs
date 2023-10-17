using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour
{
    // Reference to the GameMusic instance (singleton)
    public static GameMusic instance;

    // Audio sources for playing music
    public AudioSource music1;
    public AudioSource music2;

    // AudioClip for the death music
    public AudioClip deathMusic;

    // Speed at which music fades
    public float musicFadeSpeed = 5;

    // Boolean variables for tracking game state
    public bool danger = false;
    public bool dead = false;

    // Maximum volume for the music
    public float maxVolume = 1;

    // Optional transition audio clip and audio source
    public AudioClip optionalTransition;
    public AudioSource optionalTransitionAudioSource;

    // Countdown for transitioning from danger state to chill state
    private float countDown;

    private void Awake()
    {
        // Set the instance reference to this script
        instance = this;

        // Set the AudioListener volume to maximum
        AudioListener.volume = 1;

        // Retrieve the MaxVolume value from PlayerPrefs
        maxVolume = PlayerPrefs.GetFloat("MusicVolume");

        // Check if optional transition audio is added before proceeding
        if (!optionalTransition || !optionalTransitionAudioSource) 
            return;

        // Create an AudioSource component for the optional transition audio if none exists
        optionalTransitionAudioSource = gameObject.AddComponent<AudioSource>();
        optionalTransitionAudioSource.spatialBlend = 0.0f;
        optionalTransitionAudioSource.playOnAwake = false;
        optionalTransitionAudioSource.clip = optionalTransition;
    }

    private void Start()
    {
        // Set the outputAudioMixerGroup for music1 and music2
        music1.outputAudioMixerGroup = Game.instance.audioSource.outputAudioMixerGroup;
        music2.outputAudioMixerGroup = Game.instance.audioSource.outputAudioMixerGroup;

        // If music1 is enabled, play the music
        if (music1.enabled)
            music1.Play();

        // Set the volume of Music1 to the MaxVolume value
        music1.volume = MaxVolume;
    }

    // Called when danger is detected in the game
    public void Danger()
    {
        // Start a countdown
        countDown = 12;

        // Check if danger is not already active and the player is not dead
        if (!danger && !dead)
        {
            // Play the optional transition audio
            if (optionalTransition)
                optionalTransitionAudioSource.Play();

            // Fade the music to danger state
            StartCoroutine(FadeToDanger());

            // Set danger to true (game state)
            danger = true;
        }
    }

    // Called when the player dies
    public void Death()
    {
        if (!dead)
        {
            // Stop the current music
            if (music1.clip)
            {
                music1.Stop();
                music2.Stop();
            }

            // Set the volume and clip for music1 to play the death music
            music1.volume = 0.33f;
            music1.clip = deathMusic;

            // Enable Music1 if its disabled
            if (music1.enabled == false)
                music1.enabled = true;

            // Set the loop property to false and play the death music
            music1.loop = false;
            music1.Play();

            // Set dead to true
            dead = true;
        }
    }

    // Set the maximum volume for the music
    private void SetMaxVolume(float x)
    {
        maxVolume = x;

        // Adjust the volume of music1 or music2 based on the game state
        if (danger)
        {
            music2.volume = maxVolume;
        }
        else
        {
            music1.volume = maxVolume;
        }
    }

    // Coroutine for fading the music to the danger state
    private IEnumerator FadeToDanger()
    {
        while (true)
        {
            // Reduce the volume of Music1 over time
            music1.volume -= Time.deltaTime;

            // Increase the volume of music2 up to the maxVolume value
            if (music2.volume < maxVolume)
            {
                music2.volume += Time.deltaTime;
            }

            // Check if music1 volume has reached 0
            if (music1.volume == 0)
            {
                // Start the countdown
                StartCoroutine(CountDown());
                yield break;
            }
            yield return null;
        }
    }

    public void ForceChill()
    {
        Invoke("DelayForceChill", 1);
    }

    private void DelayForceChill()
    {
        countDown = 0;
    }

    private IEnumerator CountDown()
    {
        while (true)
        {
            // Decrease the countdown
            countDown--;

            // Check if the countdown has reached 0
            if (countDown == 0)
            {
                // Check if the player is not dead
                if (!dead)
                {
                    // Fade the music to the chill state
                    StartCoroutine(FadeToChill());
                    yield break;
                }
            }

            yield return new WaitForSeconds(1);
        }
    }

    // Coroutine for fading the music to the chill state
    private IEnumerator FadeToChill()
    {
        while (true)
        {
            // Increase the volume of Music1 up to the MaxVolume value
            if (music1.volume < maxVolume)
            {
                music1.volume += Time.deltaTime;
            }

            // Reduce the volume of music2 over time
            music2.volume -= Time.deltaTime;

            // Check if Music2 volume has reached 0
            if (music2.volume == 0)
            {
                // Set danger to false (game state)
                danger = false;
                yield break;
            }
            yield return null;
        }
    }
}
