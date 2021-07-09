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
    [SerializeField] private GameObject _interactButton = null;

    // Boolean checking wether the interact button must be displayed i.e. wether the player stands in front of the panel.
    private bool _buttonIsDisplayed = false;

    // Reference to the dialogUI which contains the rectangle box and the lines that will be displayed upon interaction.
    private DialogUIScript _dialogUI;

    // Reference to the input preferences to get the interact key.
    private InputManager _inputManager;

    // Collection of dialog lines that will appear when the panel is interacted with.
    [SerializeField] private string[] _sentences;

    // Index to keep track of the current displayed sentence.
    private int _sentences_index = 0;

    private void Start()
    {
        _dialogUI = DialogUIScript.Instance;
        _inputManager = InputManager.Instance;
    }

    private void Update()
    {
        // Begin or continue the panel dialog when the interact key is pressed while standing in front of the panel.
        //if(buttonIsDisplayed && Input.GetKeyDown(inputPreferences.interactKey))
        if (_buttonIsDisplayed && _inputManager.GetInputDown(_inputManager.InteractKey))
        {
            if(_sentences_index < _sentences.Length)
            {
                _dialogUI.DisplayNextSentence(_sentences[_sentences_index]);
                _sentences_index++;
            }
            else
            {
                // Close the dialog box
                _dialogUI.EndDialog();
                _sentences_index = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the player goes in front of the panel, the interact button is displayed can be interacted with.
        if (collision.CompareTag("Player_hurtbox") && !_buttonIsDisplayed)
        {
            _buttonIsDisplayed = true;
            _interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // When the player leaves the panel collider, wether he was interacting with the panel or not, the interact button is not displayed any longer,
        // can not be interacted with until the player comes back and the dialog lines are reset.
        if (collision.CompareTag("Player_hurtbox") && _buttonIsDisplayed)
        {
            _buttonIsDisplayed = false;
            _interactButton.SetActive(false);
            _dialogUI.EndDialog();
            _sentences_index = 0;
        }
    }

}
