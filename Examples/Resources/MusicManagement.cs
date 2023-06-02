using UnityEngine;

public class MusicManagement : MonoBehaviour
{
    public string newMusic1Path;
    public string newMusic2Path;
    public string newDeathMusicPath;
    public string newOptionalTransitionPath;

    private void Start()
    {
        // Load new music tracks, death music, and optional transition from Resources folder
        if (!string.IsNullOrEmpty(newMusic1Path))
        {
            AudioClip newMusic1 = Resources.Load<AudioClip>(newMusic1Path);
            if (newMusic1 != null)
                GameMusic.instance.music1.clip = newMusic1;
        }

        if (!string.IsNullOrEmpty(newMusic2Path))
        {
            AudioClip newMusic2 = Resources.Load<AudioClip>(newMusic2Path);
            if (newMusic2 != null)
                GameMusic.instance.music2.clip = newMusic2;
        }

        if (!string.IsNullOrEmpty(newDeathMusicPath))
        {
            AudioClip newDeathMusic = Resources.Load<AudioClip>(newDeathMusicPath);
            if (newDeathMusic != null)
                GameMusic.instance.deathMusic = newDeathMusic;
        }

        if (!string.IsNullOrEmpty(newOptionalTransitionPath))
        {
            AudioClip newOptionalTransition = Resources.Load<AudioClip>(newOptionalTransitionPath);
            if (newOptionalTransition != null)
                GameMusic.instance.optionalTransition = newOptionalTransition;
        }
    }
}
