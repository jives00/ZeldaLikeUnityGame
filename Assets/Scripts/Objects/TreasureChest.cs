using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : InteractableObjects {

    [Header("Contents")]
    public Item             contents;
    public Inventory        playerInventory;
    public bool             isOpen;
    public BoolValue        storedOpen;

    [Header("Signals and Dialog")]
    public SignalSender     raiseItem;
    public GameObject       dialogBox;
    public Text             dialogText;

    [Header("Animator")]
    private Animator        anim;

    void Start() {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.runTimeValue;
        if (isOpen) {
            anim.SetBool("opened", true);
        }
    }

    void Update() {
        if (Input.GetButtonDown("attack") && playerInRange) {
            if (!isOpen) {
                OpenChest();}
            else {
                ChestAlreadyOpen();}}
    }
    
    public void OpenChest() {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        context.Raise();
        isOpen = true;
        anim.SetBool("opened", true);
        storedOpen.runTimeValue = isOpen;
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
