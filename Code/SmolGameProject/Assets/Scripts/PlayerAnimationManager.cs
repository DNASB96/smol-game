using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    /**
     * Must be attached to : the playe game object.
     * 
     * Description : this class is used to manage the player animations.
     * The animation to play is decided depending on the player rigidbody state.
     * 
     * TODO : how to implement animations that do not depend on the player rigidbody? (e.g. attack or interact animations)
    **/

    // The player movement is used to check whether the player is grounded.
    [SerializeField] private PlayerMovementScript playerMov = null;

    // The rigidbody should only be used in Read-Only to check which animation to play.
    private Rigidbody2D rb2d;
    private Animator animator;

    // Enum-like properties for animations. These need to match the corresponding animation names.
    public static string idleAnimation = "Player_idle";
    public static string runAnimation = "Player_run";
    public static string jumpAscentAnimation = "Player_jump_ascent";
    public static string jumpDescentAnimation = "Player_jump_descent";

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // TODO : use another method than spaghetti if-else? (Finit state machine pattern?)
        if(playerMov.isGrounded)
        {
            // Idle or run animations.
            if (Mathf.Abs(rb2d.velocity.x) > 0.01)
            {
                animator.Play(runAnimation);
            }
            else
            {
                animator.Play(idleAnimation);
            }
        } else {
            // Jump animations.
            if(rb2d.velocity.y > 0)
            {
                animator.Play(jumpAscentAnimation);
            } else {
                animator.Play(jumpDescentAnimation);
            }
        }
    }
}
