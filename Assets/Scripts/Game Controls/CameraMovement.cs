using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform    target;             // set to Player in Unity
    public float        smoothing;
    public Vector2      maxPosition;
    public Vector2      minPosition;

    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    // LateUpdate is called last in the physic system (e.g. the player moves first, then the camera)
    // gets the position of where the camera should be (aligned with player)
    // sets boundaries for x, y so the camera doesn't show past the edge of the scene
    // updates camera position
    void LateUpdate() {
        if (transform.position != target.position) {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
