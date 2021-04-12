using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    /**
     * Must be attached to : the player game object.
     * 
     * Description : Class that manages all the player-related statistics.
     * 
     * The health of the player is currently not used other than by 2 temporary keys.
    **/

    // Health properties
    private int maxHealth = 100;
    private int currentHealth = 100;

    // Physics related properties
    public float runspeed = 1.5f;
    public float jumpspeed = 1f;

    // Reference to the health bar to display the correct player health.
    private HealthBarScript healthBarUI;

    private void Start()
    {
        healthBarUI = HealthBarScript.Instance;
    }

    private void Update()
    {
        // Temporary test keys for the health bar.
        if(Input.GetKeyDown(KeyCode.N))
        {
            DealDamage(5);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Heal(15);
        }
    }

    public void DealDamage(int damageAmount)
    {
        // Deal damage to the player.
        currentHealth = Mathf.Max(0, currentHealth-damageAmount);
        healthBarUI.SetHealth(currentHealth);
    }

    public void Heal(int healAmount)
    {
        // Heal the player.
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
        healthBarUI.SetHealth(currentHealth);
    }
}
