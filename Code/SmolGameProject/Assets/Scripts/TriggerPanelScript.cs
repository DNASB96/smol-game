using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPanelScript : MonoBehaviour
{
    /**
     * Must be attached to : Panel Component
     * 
     * Description : This class is used to manage the interaction with the panel. 
     * While the player stands in front of the panel, an image of the interactbutton is displayed on top of the panel to indicate that the player can interact.
     * If the player interacts with the panel, a rectangle box and dialogs are shown in the UI.
     * The player can still move while interacting with the panel but if he gets out of the panel interaction range, the UI elements will disappear and the
     * dialogs will be reset.
     **/

    // Reference to the interact button image that will appear on top of the panel while the player can interact with it.
    [SerializeField] private GameObject interactButton = null;

    // Boolean checking wether the interact button must be displayed i.e. wether the player stands in front of the panel.
    private bool buttonIsDisplayed = false;

    // Reference to the dialogUI which contains the rectangle box and the lines that will be displayed upon interaction.
    private DialogUIScript dialogUI;

    // Reference to the input preferences to get the interact key.
    private InputPreferencesScript inputPreferences;

    // Collection of dialog lines that will appear when the panel is interacted with.
    public string[] sentences;

    // Inder to keep track of the current displayed sentence.
    private int sentences_index = 0;

    private void Start()
    {
        dialogUI = DialogUIScript.Instance;
        inputPreferences = InputPreferencesScript.Instance;
    }

    private void Update()
    {
        // Begin or continue the panel dialog when the interact key is pressed while standing in front of the panel.
        if(buttonIsDisplayed && Input.GetKeyDown(inputPreferences.interactKey))
        {
            if(sentences_index < sentences.Length)
            {
                dialogUI.DisplayNextSentence(sentences[sentences_index]);
                sentences_index++;
            }
            else
            {
                // Close the dialog box
                dialogUI.EndDialog();
                sentences_index = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the player goes in front of the panel, the interact button is displayed can be interacted with.
        if (collision.CompareTag("Player_hurtbox") && !buttonIsDisplayed)
        {
            buttonIsDisplayed = true;
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // When the player leaves the panel collider, wether he was interacting with the panel or not, the interact button is not displayed any longer,
        // can not be interacted with until the player comes back and the dialog lines are reset.
        if (collision.CompareTag("Player_hurtbox") && buttonIsDisplayed)
        {
            buttonIsDisplayed = false;
            interactButton.SetActive(false);
            dialogUI.EndDialog();
            sentences_index = 0;
        }
    }

}
