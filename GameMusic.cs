using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour 
{ 
	#region VARS AND REFS
	public static GameMusic instance;
	public  AudioSource Music1;
	public AudioSource Music2 ;
	public AudioClip DeathMusic ;
	public float musicFadeSpeed =5;

	[System.NonSerialized]
	public bool danger=false;

	private bool dead=false;
	public float MaxVolume =1;
	public AudioClip optionalTransition;
	private AudioSource optionalTransitionAudioSource;
	#endregion

	#region INIT
	private void Awake()
	{
		instance = this;
		AudioListener.volume = 1;
		MaxVolume = PlayerPrefs.GetFloat ("MusicVolume");

		optionalTransitionAudioSource = gameObject.AddComponent<AudioSource> ();
		optionalTransitionAudioSource.spatialBlend = 0.0f;
		optionalTransitionAudioSource.playOnAwake = false;
		optionalTransitionAudioSource.clip = optionalTransition;
	}

	private void Start () 
	{
		Music1.outputAudioMixerGroup = Game.instance.audioSource.outputAudioMixerGroup;
		Music2.outputAudioMixerGroup = Game.instance.audioSource.outputAudioMixerGroup;

		if(Music1.enabled)
			Music1.Play();
		
		Music1.volume=MaxVolume;
	}
	#endregion

	#region METHODS
	public void Danger()
	{
		countDown = 12;

		if(!danger && !dead)
		{
			if (optionalTransition)
				optionalTransitionAudioSource.Play ();

			if(ShopKeeping.instance)
				ShopKeeping.instance.Scare();

			StartCoroutine(FadeToDanger());	
			ingozi=true;
		}
	
		GameProgress.instance.introSwitchInterrupted = true;
	}

	public void Death()
	{ 
		if(!dead)
		{
			if (Music1.clip) 
			{
				Music1.Stop ();
				Music2.Stop ();
			}
	
			Music1.volume = 0.33f;
			Music1.clip=DeathMusic;

			if (Music1.enabled == false)
				Music1.enabled = true;

			Music1.loop = false;
			Music1.Play();

			dead=true;
		} 	 
	}

	private void SetMaxVolume(float x)
	{  
		MaxVolume=x; 
		if(ingozi) 
		{
			Music2.volume=MaxVolume; 
		}
		else 
		{
			Music1.volume=MaxVolume;
		} 
	} 


	#endregion
		
	#region COROUTINES
	private IEnumerator FadeToDanger()
	{ 
		while(true)
		{ 
			Music1.volume -=Time.deltaTime;
			if(Music2.volume < MaxVolume){Music2.volume +=Time.deltaTime;}

			if(Music1.volume==0)
			{
				StartCoroutine(CountDown());
				yield break;		
			}

			yield return null;
		}
	}

	private float countDown;

	public void ForceChill()
	{
		Invoke ("ForceChill", 1);
	}

	private void _ForceChill()
	{
		countDown = 0;
	}

	private IEnumerator CountDown()
	{
		while (true) 
		{
			countDown--;

			if (countDown == 0) 
			{
				if (!dead) 
				{
					StartCoroutine (FadeToChill ());
					yield break;	
				}
			}

			yield return new WaitForSeconds(1);
		}
	}

	private IEnumerator FadeToChill()
	{
		while(true)
		{ 
			if(Music1.volume < MaxVolume){Music1.volume +=Time.deltaTime;}
			Music2.volume -=Time.deltaTime;

			if(Music2.volume==0)
			{
				ingozi=false;

				if(ShopKeeping.instance)
					ShopKeeping.instance.Recover ();
				
				yield break;	
			}

			yield return null;
		}
	}

	#endregion
}
