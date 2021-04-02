using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    /**
     * Must be attached to : the UI element that contains the health bar.
     * 
     * Description : class that manages the health bar.
     * Should be used only by the in-game object managing all the player statistics and stuff.
     * Other objects would interact with the player stats through the latter object.
     * 
     * TODO : animations of the health bar would be cool (probably when SetHealth is called).
    **/

    // Singleton instance for the player HP bar
    private static HealthBarScript _instance;
    public static HealthBarScript Instance { get { return _instance; } }

    // Slider that composes the health bar.
    private Slider curHealthSlider;

    private void Awake()
    {
        curHealthSlider = GetComponent<Slider>();

        // Ensure uniqueness by destroying duplicates
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetHealth(int newHealth)
    {
        // Values should be 
        curHealthSlider.value = newHealth;
    }
}
