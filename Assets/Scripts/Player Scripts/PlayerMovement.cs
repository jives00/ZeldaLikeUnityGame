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
    public FloatValue       currentHealth;
    public SignalSender     playerHealthSignal;
    public VectorValue      startingPosition;
    public Inventory        playerInventory;
    public SpriteRenderer   recievedItemSprite;
    public SignalSender     playerHit;
    public SignalSender     reduceMagic;
    public GameObject       projectile;

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
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    // Set movement to zero, get if it's changed from 0 and the run the move function
    void Update() {
        if (currentState == PlayerState.interact) {
            return;}
        
        change      = Vector3.zero;
        change.x    = Input.GetAxisRaw("Horizontal");
        change.y    = Input.GetAxisRaw("Vertical");

        // Attack - if attack button pressed and not currently attacking or staggered
        // Walk - if not attacking
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger) {
            StartCoroutine(AttackCo());} 
        else if (Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack && currentState != PlayerState.stagger) {
            StartCoroutine(SecondAttackCo());}
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
        if (currentState != PlayerState.interact) {
            currentState = PlayerState.walk;}
    }

    private IEnumerator SecondAttackCo() {

        // Attack
        //animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;    
        MakeArrow();
        //animator.SetBool("attacking", false);

        // Stagger, then return to walk state
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact) {
            currentState = PlayerState.walk;}
    }

    private void MakeArrow() {
        if (playerInventory.currentMagic > 0) {
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseArrowDirection());
            playerInventory.ReduceMagic(arrow.magicCost);
            reduceMagic.Raise();}
    }
 
    Vector3 ChooseArrowDirection() {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0,0,temp);
    }

    // Set signal when item received
    public void RaiseItem() {
        if (playerInventory.currentItem != null) {
            if (currentState != PlayerState.interact) {
                animator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                recievedItemSprite.sprite = playerInventory.currentItem.itemSprite;}
            else {
                animator.SetBool("receiveItem", false);
                currentState = PlayerState.idle;
                recievedItemSprite.sprite = null;
                playerInventory.currentItem = null;}}
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
    public void Knock(float knockTime, float damage) {
        currentHealth.runTimeValue -= damage;
        playerHealthSignal.Raise();

        if (currentHealth.runTimeValue > 0) {
            StartCoroutine(KnockCo(knockTime));}
        else {
            this.gameObject.SetActive(false);}
    }

    // *Private* method for when player is hit and knocked-back by an enemy
    private IEnumerator KnockCo(float knockTime) {
        // playerHit.Raise();
        if (myRigidBody != null) {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;}
    }
}
