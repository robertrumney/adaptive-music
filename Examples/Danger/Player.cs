using UnityEngine;

public class Player : MonoBehaviour
{
    private int health = 100;
    private bool dead=false;
    
    public void PlayerDamage(int damage)
    {
        health -= damage;

        // Get a reference to the GameMusic instance and trigger the Danger method
        GameMusic.instance.Danger();

        if (health <= 0)
        {
            if(!dead)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        // Get a reference to the GameMusic instance and trigger the Death method
        GameMusic.instance.Death();

        // Perform any other necessary actions upon player death
    }
}
