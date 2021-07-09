using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogUIScript : MonoBehaviour
{
    /**
     * Must be attached to : the UI object containing a canvas and a specific UI camera.
     * 
     * Description : Singleton instance that manages the UI dialogs.
     * In-game objects can use this instance to send dialog lines which are then displayed in a rectangle box on the bottom of the screen.
     * The dialog line is displayed one letter at a time to make the dialog more fluid.
    **/

    // This object must remain unique throughout the run, therefore it is a singleton.
    private static DialogUIScript _instance;
    public static DialogUIScript Instance { get { return _instance; } }

    // Game Object containing both the dialog Text and the dialog rectangle box.
    [SerializeField] private GameObject _dialog = null;

    // The game text containing the current dialog line being displayed.
    [SerializeField] private TextMeshProUGUI _dialogText = null;

    // The current sentence that needs to be displayed.
    private string _sentenceToType = "";

    private void Awake()
    {
        // Ensure uniqueness by destroying duplicates.
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void DisplayNextSentence(string sentence)
    {
        // Activate the dialog if it is not already, stop the typing of the current sentence and start the new one.
        if(!_dialog.activeSelf)
        {
            _dialog.SetActive(true);
            _dialogText.text = "";
        }
        StopAllCoroutines();
        _dialogText.text = "";
        _sentenceToType = sentence;
        StartCoroutine(TypeDialogSentence());
    }

    public void EndDialog()
    {
        // Stop typing the current sentence and hide the dialog. 
        StopAllCoroutines();
        _dialogText.text = "";
        _dialog.SetActive(false);
    }

    IEnumerator TypeDialogSentence()
    {
        // Coroutine used to display the letters of the line of dialog one by one (one per frame).
        foreach (char letter in _sentenceToType.ToCharArray())
        {
            _dialogText.text += letter;
            yield return new WaitForEndOfFrame();
        }
    }

}
