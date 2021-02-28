using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainter : PowerUp {

    public FloatValue       heartContainers;
    public FloatValue       playerHealth;

    public void OnTriggerEnter2d(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger ) {
            heartContainers.runTimeValue += 1;
            playerHealth.runTimeValue = heartContainers.runTimeValue * 2;
            powerUpSignal.Raise();
            Destroy(this.gameObject);}
    }

}
