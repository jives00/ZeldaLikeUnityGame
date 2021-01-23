using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    public float        thrust;
    public float        knockTime;
    public float        damage;

    // other refers to the object being hit
    private void OnTriggerEnter2D(Collider2D other) {

        // if the object being hit is breakable (e.g. a pot), then smash it
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player")) {
            other.GetComponent<Pot>().Smash();}
        
        // if the object being hit is an enemy or the player is hit by an enemy
        // then stagger what is being hit and knock back some ways
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player")) {

            // hit is the object being hit (enemy if playing is hitting them or player if enemy is doing the hitting)
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();

            // if something actually got hit...
            if (hit != null) {

                // for both, determine how far to move them during the knockback
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                // if enemy, stagger and knockback from Enemy class
                if (other.gameObject.CompareTag("enemy") && other.isTrigger) {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);}
                
                // if player, stagger and knockback from Player Movement class
                if (other.gameObject.CompareTag("Player")) {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponent<PlayerMovement>().Knock(knockTime);}
            }
        }
    }

}
