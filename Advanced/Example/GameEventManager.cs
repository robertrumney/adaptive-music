using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    private void Start()
    {
        // Start the game with "Chill" music.
        AdvancedGameMusic.instance.ChangeMusicState(AdvancedGameMusic.MusicState.Chill);
    }

    private void Update()
    {
        // These are just example triggers, you can replace them with actual game events.
        
        // When the player enters a dangerous zone.
        if (Input.GetKeyDown(KeyCode.D))
        {
            AdvancedGameMusic.instance.ChangeMusicState(AdvancedGameMusic.MusicState.Danger);
        }

        // When a battle starts.
        if (Input.GetKeyDown(KeyCode.B))
        {
            AdvancedGameMusic.instance.ChangeMusicState(AdvancedGameMusic.MusicState.Battle);
        }

        // When the player wins the battle.
        if (Input.GetKeyDown(KeyCode.V))
        {
            AdvancedGameMusic.instance.ChangeMusicState(AdvancedGameMusic.MusicState.Victory);
        }

        // When the player dies.
        if (Input.GetKeyDown(KeyCode.X))
        {
            AdvancedGameMusic.instance.ChangeMusicState(AdvancedGameMusic.MusicState.Death);
        }

        // Return to chill music.
        if (Input.GetKeyDown(KeyCode.C))
        {
            AdvancedGameMusic.instance.ChangeMusicState(AdvancedGameMusic.MusicState.Chill);
        }
    }
}
