using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour {

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    // When player approaches sign and hits space, display it (set to true)
    // If already displaying dialog and hits space, turn dialog box off
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange) {
            if (dialogBox.activeInHierarchy) {
                dialogBox.SetActive(false);} 
            else {
                dialogBox.SetActive(true);
                dialogText.text = dialog;}
        }
    }

    // These two functions are built in Unity functions to determine 
    // when a collision happens (Enter) and ends (Exit)
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;}
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            dialogBox.SetActive(false);}
    }
}
