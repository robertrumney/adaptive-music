using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameMusic gameMusic;

    private void Start()
    {
        // Get a reference to the GameMusic instance
        gameMusic = GameMusic.instance;

        // Initialize the boss health
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Boss AI logic here
        // Implement boss movement, attacks, etc.
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the boss is defeated
        if (currentHealth <= 0)
        {
            // Call the ForceChill method on the GameMusic script
            gameMusic.ForceChill();

            // Perform any other necessary actions upon boss defeat
            // e.g., play victory animation, trigger level completion, etc.
        }
    }
}
