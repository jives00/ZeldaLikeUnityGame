using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : InteractableObjects {

    public GameObject       dialogBox;
    public Text             dialogText;
    public string           dialog;

    // When player approaches sign and hits space, display it (set to true)
    // If already displaying dialog and hits space, turn dialog box off
    void Update() {
        if (Input.GetButtonDown("attack") && playerInRange) {
            if (dialogBox.activeInHierarchy) {
                dialogBox.SetActive(false);} 
            else {
                dialogBox.SetActive(true);
                dialogText.text = dialog;}}
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);}
    }
}
