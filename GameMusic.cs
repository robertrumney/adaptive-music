using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour 
{ 
    #region VARS AND REFS

    // Reference to the GameMusic instance (singleton)
    public static GameMusic instance;

    // Audio sources for playing music
    public AudioSource Music1;
    public AudioSource Music2;

    // AudioClip for the death music
    public AudioClip DeathMusic;

    // Speed at which music fades
    public float musicFadeSpeed = 5;

    // Boolean variables for tracking game state
    [System.NonSerialized]
    public bool danger = false;
    private bool dead = false;

    // Maximum volume for the music
    public float MaxVolume = 1;

    // Optional transition audio clip and audio source
    public AudioClip optionalTransition;
    private AudioSource optionalTransitionAudioSource;

    #endregion

    #region INIT

    private void Awake()
    {
        // Set the instance reference to this script
        instance = this;

        // Set the AudioListener volume to maximum
        AudioListener.volume = 1;

        // Retrieve the MaxVolume value from PlayerPrefs
        MaxVolume = PlayerPrefs.GetFloat("MusicVolume");

        // Create an AudioSource component for the optional transition audio
        optionalTransitionAudioSource = gameObject.AddComponent<AudioSource>();
        optionalTransitionAudioSource.spatialBlend = 0.0f;
        optionalTransitionAudioSource.playOnAwake = false;
        optionalTransitionAudioSource.clip = optionalTransition;
    }

    private void Start()
    {
        // Set the outputAudioMixerGroup for Music1 and Music2
        Music1.outputAudioMixerGroup = Game.instance.audioSource.outputAudioMixerGroup;
        Music2.outputAudioMixerGroup = Game.instance.audioSource.outputAudioMixerGroup;

        // If Music1 is enabled, play the music
        if (Music1.enabled)
            Music1.Play();

        // Set the volume of Music1 to the MaxVolume value
        Music1.volume = MaxVolume;
    }

    #endregion

    #region METHODS

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

            // Trigger scare in a shopkeeper if it exists
            if (ShopKeeping.instance)
                ShopKeeping.instance.Scare();

            // Fade the music to danger state
            StartCoroutine(FadeToDanger());

            // Set ingozi to true (game state)
            ingozi = true;
        }

        // Interrupt the intro switch in the game progress
        GameProgress.instance.introSwitchInterrupted = true;
    }

    // Called when the player dies
    public void Death()
    {
        if (!dead)
        {
            // Stop the current music
            if (Music1.clip)
            {
                Music1.Stop();
                Music2.Stop();
            }

            // Set the volume and clip for Music1 to play the death music
            Music1.volume = 0.33f;
            Music1.clip = DeathMusic;

            // Enable Music1 if it's disabled
            if (Music1.enabled == false)
                Music1.enabled = true;

            // Set the loop property to false and play the death music
            Music1.loop = false;
            Music1.Play();

            // Set dead to true
            dead = true;
        }
    }

    // Set the maximum volume for the music
    private void SetMaxVolume(float x)
    {
	MaxVolume = x;
	    // Adjust the volume of Music1 or Music2 based on the game state
    if (ingozi) 
    {
        Music2.volume = MaxVolume; 
    }
    else 
    {
        Music1.volume = MaxVolume;
    } 
} 

#endregion
	
#region COROUTINES

// Coroutine for fading the music to the danger state
private IEnumerator FadeToDanger()
{ 
    while (true)
    { 
        // Reduce the volume of Music1 over time
        Music1.volume -= Time.deltaTime;

        // Increase the volume of Music2 up to the MaxVolume value
        if (Music2.volume < MaxVolume)
        {
            Music2.volume += Time.deltaTime;
        }

        // Check if Music1 volume has reached 0
        if (Music1.volume == 0)
        {
            // Start the countdown
            StartCoroutine(CountDown());
            yield break;		
        }

        yield return null;
    }
}

// Countdown method for transitioning from danger state to chill state
private float countDown;

public void ForceChill()
{
    Invoke("ForceChill", 1);
}

private void _ForceChill()
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
        if (Music1.volume < MaxVolume)
        {
            Music1.volume += Time.deltaTime;
        }

        // Reduce the volume of Music2 over time
        Music2.volume -= Time.deltaTime;

        // Check if Music2 volume has reached 0
        if (Music2.volume == 0)
        {
            // Set ingozi to false (game state)

            ingozi = false;

            // Recover the shopkeeper if it exists
            if (ShopKeeping.instance)
                ShopKeeping.instance.Recover();

            yield break;	
        }

        yield return null;
    }
}

#endregion
