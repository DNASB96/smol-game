using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyLamia : MonoBehaviour
{
    /**
     * Must be attached to : a lamia ennemy.
     * 
     * Description : this class manages the ennemy as a finite state machine.
     * States are : Idle, Chase and Attack.
    **/

    #region Stats
    //private int maxHealth = 100;
    //private int currentHealth = 100;
    #endregion

    #region FSM
    private FSMState _currentState;
    private LamiaStateIdle _idleState;
    private FSMState _nextState;

    public FSMState CurrentState { get { return _currentState; } }
    public LamiaStateIdle IdleState { get { return _idleState; } }
    #endregion

    #region Physics
    private Rigidbody2D _rb2d;
    #endregion

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _idleState = new LamiaStateIdle(this);
        _currentState = _idleState;
        _currentState.OnEnterState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
