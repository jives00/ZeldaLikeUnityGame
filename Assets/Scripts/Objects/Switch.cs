using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public bool             active;
    public BoolValue        storedValue;
    public Sprite           activeSprite;
    private SpriteRenderer  mySprite;
    public Door             thisDoor;

    void Start() {
        mySprite    = GetComponent<SpriteRenderer>();
        active      = storedValue.runTimeValue;
        if (active) {
            ActivateSwitch();}
    }

    public void ActivateSwitch() {
        active = true;
        storedValue.runTimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;      
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            ActivateSwitch();}
    }


}
