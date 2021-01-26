using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("breakable")) {
            Debug.Log("player hit running");
            other.GetComponent<Pot>().Smash();}
    }
}
