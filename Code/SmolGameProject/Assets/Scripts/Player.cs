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

    #region Singleton Instance
    private static Player _instance;
    public static Player Instance { get { return _instance; } }
    #endregion

    #region Stats
    private readonly int _maxHealth = 100;
    private int _currentHealth = 100;
    private readonly float _runspeed = 1.2f;
    private readonly float _jumpspeed = 4f;
    #endregion

    #region FSM
    private FSMState _nextState;
    private FSMState _currentState;
    private PlayerStateIdle _idleState;
    private PlayerStateRun _runState;
    private PlayerStateJump _jumpState;
    private PlayerStateAirborne _airborneState;
    private PlayerStateCutscene _cutsceneState;

    public FSMState CurrentState { get { return _currentState; } }
    public PlayerStateIdle IdleState { get { return _idleState; } }
    public PlayerStateRun RunState { get { return _runState; } }
    public PlayerStateJump JumpState { get { return _jumpState; } }
    public PlayerStateAirborne AirborneState { get { return _airborneState; } }
    public PlayerStateCutscene CutsceneState { get { return _cutsceneState; } }

    private bool _triggeredCutscene = false;
    public bool TriggeredCutscene { get { return _triggeredCutscene; } }
    #endregion

    #region Grounded
    // Wether the player is on the ground.
    private bool _isGrounded = false;
    public bool IsGrounded { get { return _isGrounded; } }

    // 3 Points slightly underneath the player to check if the player is grounded.
    [SerializeField] private Transform _groundCheckC = null;
    [SerializeField] private Transform _groundCheckR = null;
    [SerializeField] private Transform _groundCheckL = null;
    #endregion

    #region Animation
    private Animator _animator;
    public const string idleAnimation = "Player_idle";
    public const string runAnimation = "Player_run";
    public const string jumpAscentAnimation = "Player_jump_ascent";
    public const string airborneAnimation = "Player_jump_descent";
    #endregion

    #region Physics
    private Rigidbody2D _rb2d;
    private bool _isFalling = false;
    public bool IsFalling { get { return _isFalling; } }
    #endregion

    #region UI elements
    // Reference to the health bar to display the correct player health.
    private HealthBarScript _healthBarUI;
    #endregion

    #region Input
    private InputManager _inputManager;
    #endregion


    private void Awake()
    {
        // Singleton verification
        if (_instance != null && _instance != this)
        {
            Debug.Log("Player duplicate destroyed");
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _healthBarUI = HealthBarScript.Instance;
        _animator = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        _inputManager = InputManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        // States depends on inputpreferences, so they need to be initialized in start instead of awake
        _idleState = new PlayerStateIdle(this);
        _runState = new PlayerStateRun(this);
        _jumpState = new PlayerStateJump(this);
        _airborneState = new PlayerStateAirborne(this);
        _cutsceneState = new PlayerStateCutscene(this);
        _currentState = _idleState;
        _currentState.OnEnterState();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics2D.Linecast(transform.position, _groundCheckC.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, _groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, _groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"));
        _isFalling = _rb2d.velocity.y < 0 && !_isGrounded;

        _nextState = _currentState.GetNext();
        if(_nextState != _currentState)
        {
            _currentState = _nextState;
            _currentState.OnEnterState();
        }
        _currentState.StateUpdate();

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
        _currentState.StateFixedUpdate();
    }

    public void DealDamage(int damageAmount)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - damageAmount);
        _healthBarUI.SetHealth(_currentHealth);
    }

    public void Heal(int healAmount)
    {
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + healAmount);
        _healthBarUI.SetHealth(_currentHealth);
    }

    public void PlayAnim(string animationName)
    {
        _animator.Play(animationName);
    }

    #region Movement
    public void Run(bool toRight)
    {
        // Make the player run.
        // The flipPlayer parameter must be either 1 or-1 and is used to flip the player (and all its sub components such as hitboxes, sprites, etc).
        int flipPlayer = toRight ? 1 : -1;
        _rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rb2d.velocity = new Vector2(flipPlayer * _runspeed, _rb2d.velocity.y);
        transform.localScale = new Vector3(flipPlayer * 1f, 1f, 1f);
    }

    public void Jump()
    {
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpspeed);
    }

    public void FreezePosition()
    {
        _rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }

    public void FreezeHorizontalMovement()
    {
        _rb2d.velocity = new Vector2(0, _rb2d.velocity.y);
    }

    public void Turn(bool toRight)
    {
        transform.localScale = new Vector3((toRight ? 1 : -1) * 1f, 1f, 1f);
    }
    #endregion

    #region Cutscene utilities
    public void TriggerCutscene(GameObject positionToReach)
    {
        //_cutsceneState.SetPositionToReach(positionToReach);
        //_currentState = _cutsceneState;
        // OnEnterState will not be called in the update function, so it is called now.
        //_currentState.OnEnterState();

        _cutsceneState.SetPositionToReach(positionToReach);
        _triggeredCutscene = true;
        _inputManager.LockInput();
    }
    #endregion
}
