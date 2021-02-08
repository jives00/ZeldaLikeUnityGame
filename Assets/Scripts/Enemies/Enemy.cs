using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour {

    [Header("State Machine")]
    public EnemyState   currentState;
    
    [Header("Enemy Stats")]
    public FloatValue   maxHealth;
    public float        health;
    public string       enemyName;
    public int          baseAttack;
    public float        moveSpeed;
    public Vector2      homePosition;

    [Header("Death Effects")]
    public GameObject   deathEffect;
    private float       deathEffectDelay = 1f;
    public LootTable    thisLoot;

    [Header("Death Signals")]
    public SignalSender roomSignal;

    // similar to start but isn't overriden by subclasses (e.g. log.cs)
    private void Awake() {
        health = maxHealth.initialValue;
        homePosition = transform.position;
    }

    void OnEnable() {
        transform.position = homePosition;
        health = maxHealth.initialValue;
        currentState = EnemyState.idle;
    }

    // public version that can be called by other classes when knockback occurs
    public void Knock(Rigidbody2D myRigidBody, float knockTime, float damage) {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
        TakeDamage(damage);
    }

    // performs the knockback
    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime) {
        if (myRigidBody != null) {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidBody.velocity = Vector2.zero;}
    }

    // calculates health after damage is taken
    private void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            DeathEffect();
            MakeLoot();
            if (roomSignal != null) {roomSignal.Raise();}
            this.gameObject.SetActive(false);}
    }

    private void MakeLoot() {
        if (thisLoot != null) {
            PowerUp current = thisLoot.lootPowerUp();
            if (current != null) {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }


    private void DeathEffect() {
        if (deathEffect != null) {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }


}