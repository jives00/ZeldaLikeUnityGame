using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType {
    key,
    enemy,
    button
}

public class Door : InteractableObjects {

    [Header("Door Variables")]
    public DoorType         thisDoorType;
    public bool             open = false;
    public Inventory        playerInventory;
    public SpriteRenderer   doorSprite;
    public BoxCollider2D    physicsCollider;


    // if near door, has a key and spacebar pressed then call the Open() method
    private void Update() {
        playerInventory.numberOfKeys++;
        if (Input.GetButtonDown("attack")) {
            if (playerInRange && thisDoorType == DoorType.key) {
                if (playerInventory.numberOfKeys > 0) {
                    playerInventory.numberOfKeys--;
                    Open();}}}
    }

    // turn off sprite render, making it look like door is open.  turn off box collider
    public void Open() {
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
    }

    public void Close() {
        doorSprite.enabled = true;
        open = false;
        physicsCollider.enabled = enabled;
    }
}
