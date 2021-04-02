using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    /**
     * Must be attached to : the player game object.
     * 
     * Description : this class manages the movement of the player.
     * The player can run left or right, stay idle or jump if he is grounded.
    **/

    // Reference to the input preferences.
    private InputPreferencesScript inputPreferences;

    // Reference to the player stats script.
    [SerializeField] private PlayerStatsScript playerStats = null;

    // Rigidbody of the player which is used to apply physics.
    private Rigidbody2D rb2d;

    // Wether the player is on the ground.
    public bool isGrounded { get; private set; } = false;

    // 3 Points slightly underneath the player to check if the player is grounded.
    [SerializeField] private Transform groundCheckC = null;
    [SerializeField] private Transform groundCheckR = null;
    [SerializeField] private Transform groundCheckL = null;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Input preferences are retrieved only on start to make sure it has been correctly instanciated as it uses Awake().
        inputPreferences = InputPreferencesScript.Instance;
    }

    private void FixedUpdate()
    {
        // TODO : spaghetti if-else?

        // Actions available can differ wether the player is grounded or not.
        isGrounded = CheckIsGrounded();

        if(Input.GetKey(inputPreferences.rightKey))
        {
            // Move right
            Run(1);
        } else if(Input.GetKey(inputPreferences.leftKey))
        {
            // Go left
            Run(-1);
        } else {
            // Stop movement and freeze the position to prevent the player from slipping on slopes.
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }

        // Jump action
        if(Input.GetKey(inputPreferences.jumpKey) && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, playerStats.jumpspeed);
        }

    }

    private bool CheckIsGrounded()
    {
        // Check wether there is ground between the player and one of the 3 designated points underneath the player.
        if(Physics2D.Linecast(transform.position, groundCheckC.position, 1 << LayerMask.NameToLayer("Ground") ) ||
            Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            return true;
        }
        return false;
    }

    private void Run(float flipPlayer)
    {
        // Make the player run.
        // The flipPlayer parameter must be either 1 or-1 and is used to flip the player (and all its sub components such as hitboxes, sprites, etc).
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb2d.velocity = new Vector2(flipPlayer * playerStats.runspeed, rb2d.velocity.y);
        transform.localScale = new Vector3(flipPlayer * 1f, 1f, 1f);
    }

}
