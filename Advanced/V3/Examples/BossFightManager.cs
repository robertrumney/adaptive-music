using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    private bool bossEngaged = false;
    private bool bossDefeated = false;
    
    // Reference to the boss's health component (assuming it exists)
    public BossHealth bossHealth;
    
    private void Update()
    {
        if(!bossEngaged && Vector3.Distance(transform.position, bossHealth.transform.position) < 50f)
        {
            // Player is near the boss, start the boss fight music
            UltimateDynamicGameMusic.PlayState(UltimateDynamicGameMusic.MusicState.Danger);
            bossEngaged = true;
        }

        if(bossEngaged && !bossDefeated && bossHealth.currentHealth <= 0)
        {
            // Boss is defeated
            UltimateDynamicGameMusic.PlayState(UltimateDynamicGameMusic.MusicState.Victory);
            bossDefeated = true;
        }

        if(bossDefeated && Vector3.Distance(transform.position, bossHealth.transform.position) > 100f)
        {
            // Player has moved away from the defeated boss
            UltimateDynamicGameMusic.PlayState(UltimateDynamicGameMusic.MusicState.Chill);
        }
    }
    
    // Other boss fight related methods...
}
