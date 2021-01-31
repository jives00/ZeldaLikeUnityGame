using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp {

    public FloatValue       playerHealth;
    public float            amountToIncrease;
    public FloatValue       heartContainers;

    // 'other' refers to what has entered the trigger area
    // if it is the player and not the trigger area
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            playerHealth.runTimeValue += amountToIncrease;
            if (playerHealth.runTimeValue > heartContainers.runTimeValue * 2f) {
                playerHealth.runTimeValue = heartContainers.runTimeValue * 2f;}
            powerUpSignal.Raise();
            Destroy(this.gameObject);}
    }

}
