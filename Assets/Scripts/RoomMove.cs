using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour {

    // Variables to move player between rooms
    public Vector2          cameraChange;
    public Vector3          playerChange;
    private CameraMovement  cam;

    // Variables to display place name
    public bool             needText;
    public string           placeName;
    public GameObject       text;
    public Text             placeText;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update() {}

    // Moves player/camera and calls the room name if needed
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (needText) {StartCoroutine(placeNameCo());}
        }
    }

    // Displays room name for 2 seconds
    private IEnumerator placeNameCo() {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
    }
}
