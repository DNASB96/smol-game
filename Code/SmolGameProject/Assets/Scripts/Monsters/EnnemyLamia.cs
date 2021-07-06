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
    public FSMState currentState { get; private set; }
    public LamiaStateIdle idleState { get; private set; }
    private FSMState nextState;
    #endregion

    #region Physics
    private Rigidbody2D rb2d;
    #endregion

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        idleState = new LamiaStateIdle(this);
        currentState = idleState;
        currentState.OnEnterState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
