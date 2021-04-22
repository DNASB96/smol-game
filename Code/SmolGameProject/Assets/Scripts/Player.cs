using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /**
     * Must be attached to : the player game object.
     * 
     * Description : this class manages the player as a finite state machine.
     * States are : Idle, Run and Jump.
    **/
    #region Stats
    private int maxHealth = 100;
    private int currentHealth = 100;
    private float runspeed = 1.5f;
    private float jumpspeed = 4f;
    #endregion

    #region FSM
    public PlayerState currentState { get; private set; }
    public PlayerStateIdle idleState { get; private set; }
    public PlayerStateRun runState { get; private set; }
    public PlayerStateJump jumpState { get; private set; }
    public PlayerStateAirborne airborneState { get; private set; }
    private PlayerState nextState;
    #endregion

    #region Grounded
    // Wether the player is on the ground.
    public bool isGrounded { get; private set; } = false;
    // 3 Points slightly underneath the player to check if the player is grounded.
    [SerializeField] private Transform groundCheckC = null;
    [SerializeField] private Transform groundCheckR = null;
    [SerializeField] private Transform groundCheckL = null;
    #endregion

    #region Animation
    private Animator animator;
    #endregion

    #region Physics
    private Rigidbody2D rb2d;
    public bool isFalling { get; private set; } = false;
    #endregion

    #region UI elements
    // Reference to the health bar to display the correct player health.
    private HealthBarScript healthBarUI;
    #endregion


    private void Awake()
    {
        healthBarUI = HealthBarScript.Instance;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // States depends on inputpreferences, so they need to be initialized in start instead of awake
        idleState = new PlayerStateIdle(this);
        runState = new PlayerStateRun(this);
        jumpState = new PlayerStateJump(this);
        airborneState = new PlayerStateAirborne(this);
        currentState = idleState;
        currentState.OnEnterState();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Linecast(transform.position, groundCheckC.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"));
        isFalling = rb2d.velocity.y < 0 && !isGrounded;

        nextState = currentState.GetNext();
        if(nextState != currentState)
        {
            currentState = nextState;
            currentState.OnEnterState();
        }
        currentState.StateUpdate();

        print("Current state : " + currentState);

        #region Temp
        // Temporary test keys for the health bar.
        if (Input.GetKeyDown(KeyCode.N))
        {
            DealDamage(5);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Heal(15);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        currentState.StateFixedUpdate();
    }

    public void DealDamage(int damageAmount)
    {
        currentHealth = Mathf.Max(0, currentHealth - damageAmount);
        healthBarUI.SetHealth(currentHealth);
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
        healthBarUI.SetHealth(currentHealth);
    }

    public void PlayAnim(string animationName)
    {
        animator.Play(animationName);
    }

    public void Run(float flipPlayer)
    {
        // Make the player run.
        // The flipPlayer parameter must be either 1 or-1 and is used to flip the player (and all its sub components such as hitboxes, sprites, etc).
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb2d.velocity = new Vector2(flipPlayer * runspeed, rb2d.velocity.y);
        transform.localScale = new Vector3(flipPlayer * 1f, 1f, 1f);
    }

    public void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpspeed);
    }

    public void FreezePosition()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }

    public void FreezeHorizontalMovement()
    {
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
    }
}
