using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : InteractableObjects {

    public Item             contents;
    public Inventory        playerInventory;
    public bool             isOpen;
    public SignalSender     raiseItem;
    public GameObject       dialogBox;
    public Text             dialogText;
    private Animator        anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange) {
            if (!isOpen) {
                OpenChest();}
            else {
                ChestAlreadyOpen();}}
    }
    
    public void OpenChest() {
        // Turn on dialog window and set text
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
    
        // Add contents to player inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;

        // Raise signal to player animate and set chest to open
        raiseItem.Raise();
        isOpen = true;
        anim.SetBool("opened", true);

        // Raise the context clue to get rid of ? bubble
        context.Raise();
    }

    public void ChestAlreadyOpen() {
            dialogBox.SetActive(false);
            raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen) {
            context.Raise();
            playerInRange = true;}
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen) {
            context.Raise();
            playerInRange = false;}
    }
}
