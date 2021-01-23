using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player can be in any of these states
public enum PlayerState {
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour {

    public PlayerState      currentState;
    public float            speed;

    private Rigidbody2D     myRigidBody;
    private Vector3         change;
    private Animator        animator;

    // Start is called before the first frame update
    // Grab the animator and rigidbody from whatever this script is attached to (probably just player)
    void Start() {
        currentState    = PlayerState.walk;
        animator        = GetComponent<Animator>();
        myRigidBody     = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    // Set movement to zero, get if it's changed from 0 and the run the move function
    void Update() {
        change      = Vector3.zero;
        change.x    = Input.GetAxisRaw("Horizontal");
        change.y    = Input.GetAxisRaw("Vertical");

        // Attack - if attack button pressed and not currently attacking or staggered
        // Walk - if not attacking
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger) {
            StartCoroutine(AttackCo());} 
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle) {
            UpdateAnimationAndMove();}
    }

    // Coroutine to attack (coroutines run parrell to the main process)
    private IEnumerator AttackCo() {

        // Attack
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;    
        animator.SetBool("attacking", false);

        // Stagger, then return to walk state
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    // Animation when Player is walking
    void UpdateAnimationAndMove() {
        if (change != Vector3.zero) {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);} 
        else {
            animator.SetBool("moving", false);}
    }

    // Moves the character when walking
    void MoveCharacter() {
        change.Normalize();
        myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    // *Public* method for when player is hit and knocked-back by an enemy
    public void Knock(float knockTime) {
        StartCoroutine(KnockCo(knockTime));
    }

    // *Private* method for when player is hit and knocked-back by an enemy
    private IEnumerator KnockCo(float knockTime) {
        if (myRigidBody != null) {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;}
    }
}
